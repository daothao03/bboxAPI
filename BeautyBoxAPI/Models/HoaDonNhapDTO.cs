using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class HoaDonNhapDTO
    {
        public int NccId { get; set; }

        public int MaSanPham { get; set; }

        public int Soluong { get; set; }

        [Required, Precision(16, 2)]
        public decimal GiaNhap { get; set; }

        [Required, MaxLength(100)]
        public string PaymentMethod { get; set; } = "";

        [Required, MaxLength(100)]
        public string PaymentStatus { get; set; } = "";
       

    }
}
