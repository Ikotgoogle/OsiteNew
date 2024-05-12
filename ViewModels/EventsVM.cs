using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class EventsVM {
        public List<Event> Events { get; set; }
        public User? User { get; set; }
    }
}
