using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class HomeVM {
        public List<Post> Posts { get; set; }
        public User LogUser { get; set; }
        public List<User> Users { get; set; }
    }
}
