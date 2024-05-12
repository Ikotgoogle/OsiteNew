using Microsoft.AspNetCore.Mvc;
using OsiteNew.Models;

namespace OsiteNew.Controllers {
    public class ProfileController : Controller {
        private MyAppContext _context;
        public ProfileController(MyAppContext context) {
            _context = context;
        }

        public async Task<IActionResult> ProfilePage() {
            var logUserId = User.Identity.Name;
            User logUser = await _context.Users.FindAsync(Int32.Parse(logUserId));
            return View(logUser);
        }

        public async Task<IActionResult> NewPost() {
            return RedirectToAction("NewPost", "Home");
        }
    }
}
