using Microsoft.AspNetCore.Mvc;

namespace OsiteNew.Controllers {
    public class ProfileController : Controller {
        public IActionResult ProfilePage() {
            return View();
        }
    }
}
