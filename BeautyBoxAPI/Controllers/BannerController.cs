using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Text.Json;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public BannerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        [HttpPost]
        public IActionResult CreateBanner([FromForm] BannerDTO bannerDTO)
        {
            //if (bannerDTO.ImageFileName == null || bannerDTO.ImageFileName.Count == 0)
            //{
            //    ModelState.AddModelError("ImageFiles", "Ít nhất một tệp hình ảnh là bắt buộc!");
            //    return BadRequest(ModelState);
            //}

            //string image = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //Tên hình ảnh bằng năm ngày giờ.. hiện tại
            //image += Path.GetExtension(bannerDTO.ImageFileName.FileName); //lấy đuôi mở rộng của hình ảnh trong productsDTO

            //string iFolder = env.WebRootPath + "/images/productsImage/";

            //using (var FolderNew = System.IO.File.Create(iFolder + image))
            //{
            //    bannerDTO.ImageFileName.CopyTo(FolderNew);
            //}

            //context.Banner.Add(banner);
            //context.SaveChanges();

            //return Ok();

            if (bannerDTO.ImageFileName == null || bannerDTO.ImageFileName.Count == 0)
            {
                ModelState.AddModelError("ImageFiles", "Ít nhất một tệp hình ảnh là bắt buộc!");
                return BadRequest(ModelState);
            }

            List<string> imageFileNames
                = new List<string>();
            string iFolder = env.WebRootPath + "/images/banner/";

            foreach (var formFile in bannerDTO.ImageFileName)
            {
                string imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                imageFileName += Path.GetExtension(formFile.FileName);

                using (var fileStream = System.IO.File.Create(Path.Combine(iFolder, imageFileName)))
                {
                    formFile.CopyTo(fileStream);
                }

                imageFileNames.Add(imageFileName);
            }

            Banner banner = new Banner
            {
                ImageFileName = JsonSerializer.Serialize(imageFileNames)
            };

            context.Banner.Add(banner);
            context.SaveChanges();

            return Ok(banner);
        }


        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var banner = context.Banner.Find(id);

            if (banner == null)
            {
                return NotFound();
            }

            // Xác định đường dẫn đầy đủ đến tệp hình ảnh
            string iFolder = env.WebRootPath + "/images/banner/";
            string imagePath = Path.Combine(iFolder, banner.ImageFileName);

            // Xóa tệp hình ảnh nếu nó tồn tại
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            // Xóa sản phẩm trong cơ sở dữ liệu
            context.Banner.Remove(banner);
            context.SaveChanges();

            return Ok();
        }

    }
}
