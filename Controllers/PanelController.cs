using Microsoft.AspNetCore.Mvc;

namespace OsiteNew.Controllers {
    public class PanelController : Controller {
        public IActionResult PanelPage() {
            return View();
        }
    }
}
