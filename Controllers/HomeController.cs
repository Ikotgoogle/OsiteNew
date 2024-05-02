using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OsiteNew.Controllers {
    public class HomeController : Controller {
        public async Task<IActionResult> HomePage() {
            using(HttpClient client = new HttpClient()) {
                HttpResponseMessage response = await client.GetAsync("https://api.thecatapi.com/v1/images/search");
                if(response.IsSuccessStatusCode) {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    string imageUrl = data[0].url;

                    ViewBag.ImageUrl = imageUrl;
                }
            }
            return View();
        }
    }
}
