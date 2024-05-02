namespace OsiteNew.Models {
    public class Comment {
        public int Id { get; set; }
        public User Author { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
