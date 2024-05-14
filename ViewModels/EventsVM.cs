using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class EventsVM {
        public Event Event { get; set; }
        public List<Event> Events { get; set; }
        public User? User { get; set; }
        public bool IsEditMode { get; set; }
    }
}
