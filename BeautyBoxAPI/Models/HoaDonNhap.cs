using System.ComponentModel.DataAnnotations;

namespace BeautyBoxAPI.Models
{
    public class HoaDonNhap
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } 

        public int MaNguoiDung {  get; set; }

        public int MaNCC {  get; set; }

        [MaxLength(100)]
        public string PaymentMethod { get; set; } = "";

        [MaxLength(100)]
        public string PaymentStatus { get; set; } = "";

        // navigation properties
        public User User { get; set; } = null!;

        public Suppliers ncc { get; set; } = null!;

        public List<ChiTietHoaDonNhap> cthdn {  get; set; } = null!;
    }
}
