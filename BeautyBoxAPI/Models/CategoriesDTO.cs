using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class CategoriesDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MinLength(8), MaxLength(100)]
        public string Description { get; set; } = "";
    }
}
