using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // navigation properties

        //public User User { get; set; } = null!;
    }
}
