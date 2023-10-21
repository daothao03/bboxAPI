using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X500;

namespace BeautyBoxAPI.Controllers
{
    [Authorize(Roles="admin")]
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
        public IActionResult CreateHDN(HdnDTO hdnDTO)
        {
            // kiểm tra pttt
            if (!OrderHelper.PaymentMethods.ContainsKey(hdnDTO.PaymentMethod))
            {
                ModelState.AddModelError("Payment Method", "Please select a valid payment method");
                return BadRequest(ModelState);
            }

            var ncc = context.NhaCungCap.Find(hdnDTO.NccId);
            if (ncc == null)
            //if (!listSubjects.Contains(contactsDTO.Subject))
            {
                ModelState.AddModelError("Suppliers", "Please select a valid suppliers");
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

            //HDN hdn = new HDN();
            //hdn.UserId = userId;
            //hdn.NccId = ncc;
            //hdn.CreatedAt = DateTime.Now;




            return Ok();
        }
    }
}
