using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CartsController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("PaymentMethods")]
        public IActionResult GetPaymentMethods()
        {
            return Ok(OrderHelper.PaymentMethods);
        }

        [HttpGet]
        public IActionResult GetCart(string productIdentifiers)
        {
            //khởi tạo
            CartDTO cartDto = new CartDTO();
            cartDto.CartItems = new List<CartItemDTO>();
            cartDto.SubTotal = 0;
            cartDto.ShippingFee = OrderHelper.ShippingFee;
            cartDto.TotalPrice = 0;

            //chuyển đổi dạng chuỗi về dictionary
            var productDictionary = OrderHelper.GetProductDictionary(productIdentifiers);

            foreach (var pair in productDictionary)
            {
                int productId = pair.Key; // lấy về key của dic
                var product = context.Products.Find(productId); //kiểm tra key có tồn tại trong csdl
                if (product == null)
                {
                    continue;
                }


                var cartItemDto = new CartItemDTO();
                cartItemDto.product = product; //sp = sp tìm đc qua key của dic
                cartItemDto.SoLuong = pair.Value; //số lượng = value dic

                cartDto.CartItems.Add(cartItemDto); //thêm sp vào giỏ
                cartDto.SubTotal += product.Price * pair.Value; 
                cartDto.TotalPrice = cartDto.SubTotal + cartDto.ShippingFee;
            }

            return Ok(cartDto);
        }
    }
}
