using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X500;
using SendGrid.Helpers.Mail;
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

        //[HttpPost]
        //public IActionResult CreateHDN(HoaDonNhapDTO hdnDTO)
        //{
        //    //đọc thông tin người dùng từ Jwt
        //    int userId = JwtReader.GetUserId(User);
        //    var user = context.Users.Find(userId);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("Order", "Unable to create the order");
        //        return BadRequest(ModelState);
        //    }

        //    var nccId = context.Suppliers.Find(hdnDTO.MaNCC);
        //    if (nccId == null)
        //    {
        //        ModelState.AddModelError("Nhà cung cấp", "Nhập nhà cung cấp hợp lệ");
        //        return BadRequest(ModelState);
        //    }

        //    ////chuyển đổi ProductIdentifiers -> Dictionary
        //    //var productDictionary = OrderHelper.GetProductDictionary(hdnDTO.MaSanPham);

        //    var hdn = new HoaDonNhap
        //    {
        //        CreatedAt = DateTime.Now,
        //        MaNguoiDung = userId,
        //        MaNCC = hdnDTO.MaNCC,
        //        PaymentMethod = hdnDTO.PaymentMethod,
        //        PaymentStatus = hdnDTO.PaymentStatus,
        //        //cthdn = new List<ChiTietHoaDonNhap>()
        //    };

        //    //foreach (var detail in hdn.cthdn)
        //    //{
        //    //    var product = context.Products.Find(hdnDTO.MaSanPham);
        //    //    var chitiethoadonnhap = new ChiTietHoaDonNhap()
        //    //    {
        //    //        MaSanPham = product,
        //    //        SoLuong = detail.SoLuong,
        //    //        GiaNhap = hdnDTO.GiaNhap
        //    //    };
        //    //    context.chiTietHoaDonNhaps.Add(chitiethoadonnhap);
        //    //}

        //    context.hoaDonNhaps.Add(hdn);
        //    context.SaveChanges();
        //    return Ok();

        //}

        [HttpPost]
        public IActionResult CreateHDN(HoaDonNhapDTO hdnDTO)
        {
            //đọc thông tin người dùng từ Jwt
            int userId = JwtReader.GetUserId(User);
            var user = context.Users.Find(userId);
            if (user == null)
            {
                ModelState.AddModelError("Đơn hàng", "Không thể tạo đơn hàng");
                return BadRequest(ModelState);
            }

            var nccId = context.Suppliers.Find(hdnDTO.NccId);
            if (nccId == null)
            {
                ModelState.AddModelError("Nhà cung cấp", "Nhà cung cấp không tồn tại!");
                return BadRequest(ModelState);
            }

            //tạo hóa đơn nhập
            HoaDonNhap hdn = new HoaDonNhap()
            {
                MaNguoiDung = userId,
                MaNCC = nccId,
                CreatedAt = DateTime.Now,
                PaymentMethod = hdnDTO.PaymentMethod,
                PaymentStatus = hdnDTO.PaymentStatus,
                cthdn = new List<ChiTietHDN>()
            };

            //HoaDonNhap hdn = new HoaDonNhap();
            //hdn.MaNguoiDung = userId;
            //hdn.MaNCC = nccId;
            //hdn.CreatedAt = DateTime.Now;
            //hdn.PaymentMethod = hdnDTO.PaymentMethod;
            //hdn.PaymentStatus = hdnDTO.PaymentStatus;
            //hdn.cthdn = new List<ChiTietHDN>();

            //List<ChiTietHDN> cthdn = new List<ChiTietHDN>();

            //foreach (var d in hdn.cthdn)
            //{
            //    d.MaSanPham = hdnDTO.MaSanPham;
            //    d.MaHdn = hdn.Id;
            //    d.Soluong = hdnDTO.Soluong;
            //    d.GiaNhap = hdnDTO.GiaNhap;

            //    hdn.cthdn.Add(d);
            //}

            ChiTietHDN cthdn = new ChiTietHDN
            {
                MaSanPham = hdnDTO.MaSanPham,
                Soluong = hdnDTO.Soluong,
                GiaNhap = hdnDTO.GiaNhap
            };

            hdn.cthdn.Add(cthdn);

            context.HoaDonNhap.Add(hdn);
            context.SaveChanges();

            return Ok(hdn);
        }
    }
}
