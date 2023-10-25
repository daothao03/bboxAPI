using Microsoft.EntityFrameworkCore;

namespace BeautyBoxAPI.Models
{
    public class ChiTietHDN
    {
        public int Id { get; set; }

        public int MaSanPham {  get; set; }

        public int MaHdn { get; set; }

        public int Soluong { get; set; }

        [Precision(16,2)]
        public decimal GiaNhap { get; set; }

        //navigation properties
        public Product sanpham { get; set; } = null!;

        public HoaDonNhap hdn { get; set; } = null!;
    }
}
