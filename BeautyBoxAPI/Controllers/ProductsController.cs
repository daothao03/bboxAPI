using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;

        private readonly List<string> listCategories = new List<string>()
        {
            "Trang điểm", "Chăm sóc da mặt", "Chăm sóc cơ thể", "Chăm sóc tóc & da"
            , "Chăm sóc cá nhân", "Nước hoa", "Thiết bị làm đẹp", "Thực phẩm chức năng"
            , "Dụng cụ trang điểm", "Mini/Sample"
        };

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            return Ok(listCategories);
        }

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this._context = context;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult GetProducts(string? search, string? category,
            int? minPrice, int? maxPrice, string? brand)
        {
            IQueryable<Product> query = _context.Products;

            // search functionality
            if (search != null)
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            if (category != null)
            {
                query = query.Where(p => p.Category == category);
            }

            if (minPrice != null)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            if (brand != null)
            {
                query = query.Where(p => p.Brand == brand);
            }


            var products = query.ToList();
            return Ok(products);
        }

        [HttpGet("id")]
        public IActionResult GetProducts(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromForm] ProductsDTO productsDTO)
        {
            if (!listCategories.Contains(productsDTO.Category))
            {
                ModelState.AddModelError("Category", "Please select a valid category");
                return BadRequest(ModelState);
            }

            //Vì file hình ảnh là tùy chọn => check image: null or not null
            if (productsDTO.ImageFileName == null)
            {
                ModelState.AddModelError("ImageFile", "The Image File is required!");
                return BadRequest(ModelState);
            }

            //save the image on the server
            string imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //Tên hình ảnh bằng năm ngày giờ.. hiện tại
            imageFileName += Path.GetExtension(productsDTO.ImageFileName.FileName); //lấy đuôi mở rộng của hình ảnh trong productsDTO

            string iFolder = environment.WebRootPath + "/images/productsImage/";

            using (var FolderNew = System.IO.File.Create(iFolder + imageFileName))
            {
                productsDTO.ImageFileName.CopyTo(FolderNew); 
            }

            //save the product in the database
            Product product = new Product()
            {
                Name = productsDTO.Name,
                Brand = productsDTO.Brand,
                Category = productsDTO.Category,
                Price = productsDTO.Price,
                Description = productsDTO.Description ?? "",
                ImageFileName = imageFileName,
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id,[FromForm] ProductsDTO productsDTO)
        {
            if (!listCategories.Contains(productsDTO.Category))
            {
                ModelState.AddModelError("Category", "Please select a valid category");
                return BadRequest(ModelState);
            }

            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            string imageFileName = product.ImageFileName; //đọc tên tệp hình ảnh sản phẩm lấy trong csdl
            if(productsDTO.ImageFileName != null) //kiểm tra có tên tệp mới trong productDTO không -> khác rỗng -> có thêm 1 h/a mới
            {
                // save the image on the server
                imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //Tên hình ảnh bằng năm ngày giờ.. hiện tại
                imageFileName += Path.GetExtension(productsDTO.ImageFileName.FileName); //lấy đuôi mở rộng của hình ảnh trong productsDTO

                string iFolder = environment.WebRootPath + "/images/productsImage/";

                using (var FolderNew = System.IO.File.Create(iFolder + imageFileName))
                {
                    productsDTO.ImageFileName.CopyTo(FolderNew);
                }

                // delete the old image
                System.IO.File.Delete(iFolder + product.ImageFileName); //cung cấp đường dẫn đầy đủ đến hình ảnh cũ
            }

            //update the product in the database
            product.Name = productsDTO.Name;
            product.Brand = productsDTO.Brand;
            product.Category = productsDTO.Category;
            product.Price = productsDTO.Price;
            product.Description = productsDTO.Description ?? "";
            product.ImageFileName = imageFileName;

            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null) 
            { 
                return NotFound();
            }

            //delete the image on the server
            string iFolder = environment.WebRootPath + "/images/productsImage/";
            System.IO.File.Delete(iFolder + product.ImageFileName);

            //delete the product in the database
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok();
        }
    }
}
