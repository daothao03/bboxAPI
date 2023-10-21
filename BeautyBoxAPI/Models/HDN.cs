namespace BeautyBoxAPI.Models
{
    public class HDN
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public required NhaCungCap NccId { get; set; }

        public DateTime CreatedAt { get; set; }

        // navigation properties
        public User User { get; set; } = null!;

        public List<ChiTietHDN> ChiTietHDNs { get; set; } = null!;
    }
}
