using Microsoft.EntityFrameworkCore;

namespace BeautyBoxAPI.Models
{
    public class ChiTietHoaDonNhap
    {
        public int Id { get; set; }

        public int MaSanPham {  get; set; }

        public int MaHoaDonNhap { get; set; }

        public int SoLuong {  get; set; }

        [Precision(16,2)]
        public decimal GiaNhap { get; set; }

        // navigation properties
        public Product sanpham { get; set; } = null!;

        public HoaDonNhap hdn { get; set; } = null !;

    }
}
