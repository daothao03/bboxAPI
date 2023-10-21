using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class HdnDTO
    {
        [Required]
        public string PaymentMethod { get; set; } = "";

        public int NccId { get; set; }

        public int userId { get; set; }
    }
}
