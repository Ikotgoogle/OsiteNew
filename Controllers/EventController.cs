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

        async Task<List<Event>> GetSortedEvents() {
            List<Event> events = await _context.Events.ToListAsync();
            events.Sort((ev1, ev2) => ev1.Date.CompareTo(ev2.Date));
            return events;
        }

        public async Task<IActionResult> EventPage() {
            return View(await GetSortedEvents());
        }

        public IActionResult NewEvent() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewEvent(Event newEv) {
            if(ModelState.IsValid) {
                Event ev = new Event { Title = newEv.Title, Description = newEv.Description, Place = newEv.Place, Date = newEv.Date, Time = newEv.Time };
                _context.Events.Add(ev);
                await _context.SaveChangesAsync();
            }
            return View("EventPage", await GetSortedEvents()) ;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var ev = await _context.Events.FindAsync(id);
            if(ev != null) {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }
            return View("EventPage", await GetSortedEvents());
        }
    }
}
