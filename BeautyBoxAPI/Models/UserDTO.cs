using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class UserDTO //nhận DL từ người dùng -> tạo tk
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = "";

        [Required, MaxLength(100)]
        public string LastName { get; set; } = "";

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; } = "";

        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; } = "";
    }
}
