//using BeautyBoxAPI.Models;
//using BeautyBoxAPI.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BeautyBoxAPI.Controllers
//{
//    [Authorize(Roles ="admin")]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NhaCungCapsController : ControllerBase
//    {
//        private readonly ApplicationDbContext context;

//        public NhaCungCapsController(ApplicationDbContext context)
//        {
//            this.context = context;
//        }


//        [HttpGet]
//        public IActionResult GetSupplier()
//        {
//            var listSupplier = context.NhaCungCap.ToList();
//            return Ok(listSupplier);
//        }

//        [HttpPost]
//        public IActionResult CreateSupplier(NhaCungCapDTO nhaCungCapDTO) 
//        { 
//            NhaCungCap ncc = new NhaCungCap()
//            {
//                Name = nhaCungCapDTO.Name,
//                Email = nhaCungCapDTO.Email,
//                Address = nhaCungCapDTO.Address,
//                SDT = nhaCungCapDTO.SDT
//            };

//            context.NhaCungCap.Add(ncc);
//            context.SaveChanges();

//            return Ok(ncc);
//        }

//        [HttpPut]
//        public IActionResult UpdateSupplier(int id, NhaCungCapDTO nccDTO) 
//        {
//            var ncc = context.NhaCungCap.Find(id);
//            if (ncc == null)
//            {
//                return NotFound();
//            }

//            ncc.Name = nccDTO.Name;
//            ncc.Address = nccDTO.Address;
//            ncc.Email = nccDTO.Email;
//            ncc.SDT = nccDTO.SDT;

//            context.SaveChanges();

//            return Ok();
//        }

//        [HttpDelete]
//        public IActionResult DeleteSupplier(int id)
//        {
//            try
//            {
//                var ncc = new NhaCungCap()
//                {
//                    ID = id
//                };
//                context.NhaCungCap.Remove(ncc);
//                context.SaveChanges();

//            }
//            catch (Exception)
//            {
//                return NotFound();
//            }
//            return Ok();
//        }
//    }
//}
