namespace OsiteNew.Models {
    public class HeaderButtons {
        public static readonly List<(string, string, string)> headerButtonsList =
            new List<(string controller, string action, string content)> { 
                ("Home", "HomePage", "Главная"),
                ("Event", "EventPage", "События"),
                //("Profile", "ProfilePage", "Профиль"),
            };
    }
}
