using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsiteNew.Models;

namespace OsiteNew.Controllers {
    public class PanelController : Controller {
        private MyAppContext _context;
        public PanelController(MyAppContext context) {
            _context = context;
        }

        public async Task<IActionResult> PanelPage() {
            return View();
        }

        public async Task<ActionResult> GetContent(string section) {
            string content = string.Empty;
            List<User> users = await _context.Users.ToListAsync();

            switch(section) {
                case "users":
                    return PartialView("AllUsers", users);
                case "something":
                    content = "<h1>Страница в разработке</h1>";
                    break;
            }
            return Content(content);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var p = await _context.Users.FindAsync(id);
            if(p != null) {
                string e = p.Email;
                BanListClass.BanList.Add(e);
                _context.Users.Remove(p);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PanelPage");
        }
    }
}
