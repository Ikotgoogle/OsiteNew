using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsiteNew.Models;
using OsiteNew.ViewModels;

namespace OsiteNew.Controllers {
    public class PanelController : Controller {
        private MyAppContext _context;
        private JokesVM _jokesVM = new();

        public PanelController(MyAppContext context) {
            _context = context;
        }

        public async Task<IActionResult> PanelPage() {
            return View();
        }

        public async Task<ActionResult> GetContent(string section) {
            string content = string.Empty;

            switch(section) {
                case "users":
                    AllUsersVM allUsersVM = new(_context);
                    return PartialView("AllUsers", allUsersVM);
                case "jokes":
                    _jokesVM.Jokes = await _context.Jokes.ToListAsync();
                    return PartialView("AllJokes", _jokesVM);
                default:
                    content = "<h1>Страница в разработке</h1>";
                    break;
            }
            return Content(content);
        }

        [HttpPost]
        public async Task<IActionResult> Ban(int id) {
            var p = await _context.Users.FindAsync(id);
            if(p != null) {
                _context.Users.Remove(p);
                _context.BannedUsers.Add(p);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PanelPage");
        }
        
        [HttpPost]
        public async Task<IActionResult> Unban(int id) {
            var p = await _context.Users.FindAsync(id);
            _context.BannedUsers.Remove(p);
            _context.Users.Add(p);
            await _context.SaveChangesAsync();

            return RedirectToAction("PanelPage");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewJoke(JokesVM newJoke) {
            if(ModelState.IsValid) {
                _context.Jokes.Add(newJoke.Joke);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PanelPage");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJoke(int id) {
            var joke = await _context.Jokes.FindAsync(id);
            _context.Jokes.Remove(joke);
            await _context.SaveChangesAsync();

            return RedirectToAction("PanelPage");
        }
    }
}
