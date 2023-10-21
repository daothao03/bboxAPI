using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class PromotionsDTO
    {
        [Required, MinLength(8), MaxLength(128)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string Description { get; set; } = "";

        [Required]
        public decimal Percent { get; set; }

        public DateTime CreatedAt = DateTime.Now;

        // navigation properties

        //List<int> ProductsId { get; set; } = null!;

    }
}
