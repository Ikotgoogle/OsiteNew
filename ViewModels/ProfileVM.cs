using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class ProfileVM {
        public User LogUser { get; set; }
        public List<Post> Posts { get; set; }
    }
}
