using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class UserProfileDTO //gửi DL lại cho người dùng
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Address { get; set; } = "";

        public string Role { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
