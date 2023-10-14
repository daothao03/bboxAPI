namespace BeautyBoxAPI.Models
{
    public class CartDTO
    {
        public List<CartItemDTO> CartItems { get; set; } = new(); // lấy danh sách các mặt hàng có trong cart

        public decimal SubTotal { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
