using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class SubjectsDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
    }
}
