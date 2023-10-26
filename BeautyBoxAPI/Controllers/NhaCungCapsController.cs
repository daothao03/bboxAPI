using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBoxAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public NhaCungCapsController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetSupplier(string? search , int? page)
        {
            //var listSupplier = context.Suppliers.ToList();
            //return Ok(listSupplier);
            IQueryable<Suppliers> query = context.Suppliers;

            //search
            if (search != null)
            {
                query = query.Where(s => s.SDT == search || s.Name.Contains(search) || s.Email.Contains(search));
            }

            //pagination
            if (page == null || page < 1)
            {
                page = 1;
            }

            int pageSize = 10;
            int total = 0;

            decimal count = query.Count();
            total = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((int)(page - 1) * pageSize)
                .Take(pageSize);

            var supplier = query.ToList();

            return Ok(new
            {
                ncc = supplier,
                total = total,
                page = page,
                pageSize = pageSize
            }); 
        }

        [HttpPost]
        public IActionResult CreateSupplier(SupliersDTO nhaCungCapDTO)
        {
            Suppliers ncc = new Suppliers()
            {
                Name = nhaCungCapDTO.Name,
                Email = nhaCungCapDTO.Email,
                Address = nhaCungCapDTO.Address,
                SDT = nhaCungCapDTO.SDT
            };

            context.Suppliers.Add(ncc);
            context.SaveChanges();

            return Ok(ncc);
        }

        [HttpPut]
        public IActionResult UpdateSupplier(int id, SupliersDTO nccDTO)
        {
            var ncc = context.Suppliers.Find(id);
            if (ncc == null)
            {
                return NotFound();
            }

            ncc.Name = nccDTO.Name;
            ncc.Address = nccDTO.Address;
            ncc.Email = nccDTO.Email;
            ncc.SDT = nccDTO.SDT;

            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                var ncc = new Suppliers()
                {
                    ID = id
                };
                context.Suppliers.Remove(ncc);
                context.SaveChanges();

            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
