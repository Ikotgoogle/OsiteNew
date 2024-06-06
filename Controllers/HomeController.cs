using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OsiteNew.Models;
using OsiteNew.ViewModels;

namespace OsiteNew.Controllers {
    public class HomeController : Controller {
        private MyAppContext _context;
        public HomeController(MyAppContext context) {
            _context = context;
            #region addPosts
            //Post p1 = new Post { Title = "Title1", Date = new DateOnly(2024, 5, 12), Time = new TimeOnly(12, 00), Description = "Desc1", Author = _context.Users.First(u => u.Email == "ikotskiy@gmail.ru") };
            //Post p2 = new Post { Title = "Title2", Date = new DateOnly(2024, 5, 12), Time = new TimeOnly(13, 00), Description = "Desc2", Author = _context.Users.First(u => u.Email == "test@gmail.com") };
            //Post p3 = new Post { Title = "Title3", Date = new DateOnly(2024, 5, 12), Time = new TimeOnly(11, 00), Description = "Desc3", Author = _context.Users.First(u => u.Email == "test@gmail.com") };
            //_context.Posts.AddRange(p1, p2, p3);
            //_context.SaveChanges();
            #endregion
        }

        HomeVM _homeVM = new();

        async Task<List<Post>> GetSortedPosts() {
            List<Post> posts = await _context.Posts.ToListAsync();
            List<Post> sorted = posts.OrderByDescending(p => p.Date).ThenByDescending(p => p.Time).ToList();
            return sorted;
        }

        async Task<HomeVM> GetVM() {
            _homeVM.Posts = await GetSortedPosts();
            _homeVM.Users = await _context.Users.ToListAsync();
            if(User.Identity.IsAuthenticated) {
                User logUser = await GetLogUser();
                _homeVM.LogUser = logUser;
            }
            return _homeVM;
        }

        async Task<User> GetLogUser() {
            int userId = Int32.Parse(User.Identity.Name);
            User loggedUser = await _context.Users.FindAsync(userId);
            return loggedUser; 
        }

        public async Task<IActionResult> HomePage() {
            try {
                using(HttpClient client = new HttpClient()) {
                    HttpResponseMessage response = await client.GetAsync("https://api.thecatapi.com/v1/images/search");
                    if(response.IsSuccessStatusCode) {
                        string json = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(json);
                        string imageUrl = data[0].url;
                        ViewBag.ImageUrl = imageUrl;
                    }
                }
            } catch { }

            return View(await GetVM());
        }

        [Authorize]
        public async Task<IActionResult> NewPost() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostModel newPost) {
            if (ModelState.IsValid) {
                Post post = new Post {
                    Title = newPost.Title,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    Description = newPost.Description,
                    Author = await GetLogUser(),
                    Comments = new()
                };
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("HomePage");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var p = await _context.Posts.FindAsync(id);
            if(p != null) {
                _context.Posts.Remove(p);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("HomePage");
        }
    }
}
