using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class ProfileVM {
        public User? LogUser { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public NewPostModel? Post { get; set; }
        public User? Author { get; set; }
    }
}
