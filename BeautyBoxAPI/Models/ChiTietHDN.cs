using Microsoft.EntityFrameworkCore;

namespace BeautyBoxAPI.Models
{
    public class ChiTietHDN
    {
        public int Id { get; set; }

        public int HdnId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Precision(16, 2)]
        public decimal GiaNhap { get; set; }


        // navigation properties
        public HDN hdn { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
