namespace OsiteNew.Models {
    public class Event {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly? Time { get; set; }
    }
}
