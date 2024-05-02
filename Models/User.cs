using System.ComponentModel.DataAnnotations;

namespace OsiteNew.Models {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Address { get; set; }
        public string? About { get; set; }
        public string? Avatar { get; set; }
        public string? Nickname { get; set; }
        public List<Post>? Posts { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
