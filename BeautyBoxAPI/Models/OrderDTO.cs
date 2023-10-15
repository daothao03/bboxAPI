using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class OrderDTO
    {
        [Required]
        public string ProductIdentifiers { get; set; } = "";

        [Required, MinLength(20), MaxLength(400)]
        public string DeliveryAddress { get; set; } = "";

        [Required]
        public string PaymentMethod { get; set; } = "";
    }
}
