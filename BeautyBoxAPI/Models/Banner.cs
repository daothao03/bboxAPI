using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class Banner
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";
    }
}
