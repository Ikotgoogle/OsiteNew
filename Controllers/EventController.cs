using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsiteNew.Models;

namespace OsiteNew.Controllers {
    public class EventController : Controller {
        private MyAppContext _context;
        public EventController(MyAppContext context) {
            _context = context;

            //Event ev1 = new Event { 
            //    Title = "День Рождение чела",
            //    Place = "КФС",
            //    Description = "Description",
            //    Date = new DateOnly(2024, 7, 18)
            //};

            //Event ev2 = new Event {
            //    Title = "New Year",
            //    Place = "Place2",
            //    Description = "Description",
            //    Date = new DateOnly(2024, 12, 31)
            //};

            //Event ev3 = new Event {
            //    Title = "Мое ДР",
            //    Place = "Санкт-Петербург",
            //    Description = "Description",
            //    Date = new DateOnly(2024, 12, 12)
            //};

            //_context.Events.AddRange(ev1, ev2, ev3);
            //_context.SaveChanges();
        }

        public async Task<IActionResult> EventPage() {
            List<Event> events = await _context.Events.ToListAsync();
            events.Sort((ev1, ev2) => ev1.Date.CompareTo(ev2.Date));
            return View(events);
        }
    }
}
