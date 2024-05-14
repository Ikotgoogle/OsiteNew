﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsiteNew.Models;
using OsiteNew.ViewModels;

namespace OsiteNew.Controllers {
    public class ProfileController : Controller {
        private MyAppContext _context;
        private IWebHostEnvironment _environment;
        public ProfileController(MyAppContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment; 
        }

        ProfileVM _profileVM = new();
        async Task<List<Post>> GetSortedUserPosts(int id) {
            List<Post> posts = await _context.Posts.Where(p=> p.Author.Id == id).ToListAsync();
            List<Post> sorted = posts.OrderBy(p => p.Date).ThenBy(p => p.Time).ToList();
            return sorted;
        }

        async Task<User> GetLogUser() {
            int userId = Int32.Parse(User.Identity.Name);
            User loggedUser = await _context.Users.FindAsync(userId);
            return loggedUser;
        }

        async Task<ProfileVM> GetVM() {
            User logUser = await GetLogUser();
            _profileVM.LogUser = logUser;
            _profileVM.Posts = await GetSortedUserPosts(logUser.Id);
            return _profileVM;
        }

        [Authorize]
        public async Task<IActionResult> ProfilePage() {
            return View(await GetVM());
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar) {
            if(avatar != null && avatar.Length > 0) {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create)) {
                    await avatar.CopyToAsync(fileStream); 
                }

                var logUserId = User.Identity.Name;
                User logUser = await _context.Users.FindAsync(Int32.Parse(logUserId));
                logUser.Avatar = "/img/" + uniqueFileName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ProfilePage");
        }


        public async Task<IActionResult> NewPost() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostModel newPost) {
            if(ModelState.IsValid) {
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
            return View("ProfilePage", await GetVM());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var p = await _context.Posts.FindAsync(id);
            if(p != null) {
                _context.Posts.Remove(p);
                await _context.SaveChangesAsync();
            }

            return View("HomePage", await GetVM());
        }
    }
}
