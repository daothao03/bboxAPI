using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class HoaDonNhapDTO
    {
        [Required]
        public string MaSanPham { get; set; } = "";

        [Required]
        public int SoLuong { get; set; }

        [Required, Precision(16,2)]
        public decimal GiaNhap { get; set; }

        [Required, MaxLength(100)]
        public string PaymentMethod { get; set; } = "";
    }
}
