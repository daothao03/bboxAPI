using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class Promotions
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string Description { get; set; } = "";


        [Precision(16, 2)]
        public decimal Percent {  get; set; }

        public DateTime CreatedAt = DateTime.Now;



        // navigation properties

        //List<Product> listProducts { get; set; } = new List<Product>();
    }
}
