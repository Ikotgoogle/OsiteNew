using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsiteNew.Models;
using OsiteNew.ViewModels;

namespace OsiteNew.Controllers {
    public class EventController : Controller {
        private MyAppContext _context;
        public EventController(MyAppContext context) {
            _context = context;
            #region addData
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
            #endregion
        }

        private EventsVM _eventsVM = new();

        async Task<List<Event>> GetSortedEvents() {
            List<Event> events = await _context.Events.ToListAsync();
            events.Sort((ev1, ev2) => ev1.Date.CompareTo(ev2.Date));
            return events;
        }

        async Task<EventsVM> GetVM() {
            _eventsVM.Events = await GetSortedEvents();
            if(User.Identity.IsAuthenticated) {
                int userId = Int32.Parse(User.Identity.Name);
                User loggedUser = await _context.Users.FindAsync(userId);
                _eventsVM.User = loggedUser;
            }
            return _eventsVM;
        }

        async Task<EventsVM> GetVM(Event? ev, bool IsEditMode) {
            _eventsVM.Event = ev;
            _eventsVM.IsEditMode = IsEditMode;
            _eventsVM.Events = await GetSortedEvents();
            if(User.Identity.IsAuthenticated) {
                int userId = Int32.Parse(User.Identity.Name);
                User loggedUser = await _context.Users.FindAsync(userId);
                _eventsVM.User = loggedUser;
            }
            return _eventsVM;
        }

        public async Task<IActionResult> EventPage() {
            return View(await GetVM());
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> NewEvent() {
            return View(await GetVM());
        }

        [HttpPost]
        public async Task<IActionResult> NewEvent(EventsVM newEv) {
            if(ModelState.IsValid) {
                Event ev = new Event { 
                    Title = newEv.Event.Title, 
                    Description = newEv.Event.Description, 
                    Place = newEv.Event.Place, 
                    Date = newEv.Event.Date, 
                    Time = newEv.Event.Time };
                _context.Events.Add(ev);
                await _context.SaveChangesAsync();
            }
            return View("EventPage", await GetVM()) ;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id) {
            var ev = await _context.Events.FindAsync(id);
            return View("NewEvent", await GetVM(ev, true));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEdit(EventsVM edEv, int id) {
            if(ModelState.IsValid) {
                var ev = await _context.Events.FindAsync(id);
                ev.Title = edEv.Event.Title;
                ev.Description = edEv.Event.Description;
                ev.Place = edEv.Event.Place;
                ev.Date = edEv.Event.Date;
                ev.Time = edEv.Event.Time;
                await _context.SaveChangesAsync();
            }
            else return View("NewEvent", await GetVM(await _context.Events.FindAsync(id), true));
            return RedirectToAction("EventPage");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id) {
            var ev = await _context.Events.FindAsync(id);
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();            
            return View("EventPage", await GetVM());
        }
    }
}
