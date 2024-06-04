using Microsoft.AspNetCore.Mvc;

namespace OsiteNew.Controllers {
    public class PanelController : Controller {
        public IActionResult PanelPage() {
            return View();
        }

        public ActionResult GetContent(string section) {
            string content = string.Empty;

            switch(section) {
                case "users":
                    return PartialView("AllUsers");
                case "something":
                    content = "<h1>Страница в разработке</h1>";                    
                    break;
            }

            return Content(content);
        }
    }
}
