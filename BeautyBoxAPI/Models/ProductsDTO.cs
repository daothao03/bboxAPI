using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class ProductsDTO
    {
        [Required ,MaxLength(100)]
        public string Name { get; set; } = "";

        [Required ,MaxLength(100)]
        public string Brand { get; set; } = "";

        [Required ,MaxLength(100)]
        public string Category { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        [MaxLength(5000)]
        public string? Description { get; set; }

        public int SoLuong { get; set; }

        public IFormFile? ImageFileName { get; set; }
    }
}
