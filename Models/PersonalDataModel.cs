namespace OsiteNew.Models {
    public class PersonalDataModel {
        public string Name { get; set; }
        public string? LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? About { get; set; }
        public string? Nickname { get; set; }
    }
}
