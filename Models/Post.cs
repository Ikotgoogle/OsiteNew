namespace OsiteNew.Models {
    public class Post {
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
    }
}
