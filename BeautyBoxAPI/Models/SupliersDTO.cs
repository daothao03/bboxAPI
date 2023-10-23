using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class SupliersDTO
    {
        [Required, MaxLength(1000)]
        public string Name { get; set; } = "";

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(100)]
        public string Address { get; set; } = "";

        [MaxLength(20)]
        public string SDT { get; set; } = "";
    }
}
