using OsiteNew.Models;

namespace OsiteNew.ViewModels {
    public class SettingsVM {
        public User? LogUser { get; set; }
        public PersonalDataModel? PersonalDataModel { get; set; }
        public EmailPasswordModel? EmailPasswordModel { get; set; }
    }
}
