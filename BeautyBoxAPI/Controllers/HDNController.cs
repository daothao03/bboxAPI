using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X500;
using sib_api_v3_sdk.Model;

namespace BeautyBoxAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HDNController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public HDNController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]        
        public IActionResult CreateHDN(HoaDonNhapDTO  hdnDTO)
        {
            decimal totalAmount = 0;

            // kiểm tra pttt
            if (!OrderHelper.PaymentMethods.ContainsKey(hdnDTO.PaymentMethod))
            {
                ModelState.AddModelError("Payment Method", "Please select a valid payment method");
                return BadRequest(ModelState);
            }

            //đọc thông tin người dùng từ Jwt
            int userId = JwtReader.GetUserId(User);
            var user = context.Users.Find(userId);
            if (user == null)
            {
                ModelState.AddModelError("Order", "Unable to create the order");
                return BadRequest(ModelState);
            }

            //chuyển đổi ProductIdentifiers -> Dictionary
            var productDictionary = OrderHelper.GetProductDictionary(hdnDTO.MaSanPham);

            //tạo đơn hàng
            HoaDonNhap hdn = new HoaDonNhap();
            hdn.MaNguoiDung = userId;
            hdn.CreatedAt = DateTime.Now;
            hdn.PaymentMethod = hdnDTO.PaymentMethod;
            hdn.PaymentStatus = OrderHelper.PaymentStatuses[0];

            //foreach (var item in hdn.cthdn)
            //{
            //    int productId = pair.Key;
            //    var product = context.Products.Find(productId);
            //    if (product == null)
            //    {
            //        ModelState.AddModelError("Sản phẩm", "Sản phẩm có mã " + productId + " không tồn tại");
            //        return BadRequest(ModelState);
            //    }

            //    var cthdn = new ChiTietHoaDonNhap()
            //    {
            //        MaSanPham = productId,
            //        SoLuong = p,
            //        GiaNhap = item.GiaNhap
            //    };

            //    hdn.cthdn.Add(cthdn);
            //}

            //kiểm tra sp có tồn tại trong csdl không
            foreach (var pair in productDictionary)
            {
                int productId = pair.Key;
                var product = context.Products.Find(productId);

                if (product == null)
                {
                    ModelState.AddModelError("Sản phẩm", "Sản phẩm có mã " + productId + " không tồn tại");
                    return BadRequest(ModelState);
                }

                //tạo chi tiết đơn hàng
                var cthdn = new ChiTietHoaDonNhap();
                cthdn.MaSanPham = productId;
                cthdn.SoLuong = hdnDTO.SoLuong;
                cthdn.GiaNhap = hdnDTO.GiaNhap;

                //thêm đơn hàng
                hdn.cthdn.Add(cthdn);

                // tính tổng tiền cho từng sản phẩm
                totalAmount += cthdn.GiaNhap * cthdn.SoLuong;
            }

            context.hoaDonNhaps.Add(hdn);
            context.SaveChanges();

            return Ok(hdn);
        }
    }
}
