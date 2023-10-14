namespace BeautyBoxAPI.Models
{
    public class CartItemDTO
    {
        public Product product { get; set; } = new Product();

        public int SoLuong { get; set; }
    }
}
