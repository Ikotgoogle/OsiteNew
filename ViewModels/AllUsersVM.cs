using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class AllUsersVM {
        MyAppContext _appContext;
        public AllUsersVM(MyAppContext context) {
            _appContext = context;
            BannedUsers = _appContext.BannedUsers.ToList();
            Users = _appContext.Users.ToList();
        }

        public List<User> Users { get; set; }
        public List<User> BannedUsers { get; set; } 
    }
}
