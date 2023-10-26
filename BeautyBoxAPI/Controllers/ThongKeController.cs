using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautyBoxAPI.Controllers
{
    [Authorize(Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ThongKeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //[HttpGet("TongHoaDon")]
        //public IActionResult TongHoaDon()
        //{
        //    decimal total = context.Orders.Count();
        //    return Ok(total);
        //}

        //[HttpGet("TongKhachHang")]
        //public IActionResult TongKhachHang()
        //{
        //    IQueryable<Order> query = context.Orders.Include(o => o.User);

        //    decimal total = query.Count();
           
        //    return Ok(total);
        //}

        //[HttpGet("DoanhThu")]
        //public IActionResult DoanhThu()
        //{
        //    //IQueryable<Order> query = context.Orders.Include(o => o.User)
        //    //    .Include(o => o.OrderItems).ThenInclude(oi => oi.Product);

        //    //var total = query.Sum(o => o.Price);
        //    //return Ok(total);
        //    decimal total = context.OrderItems.Sum(orderItem => orderItem.Quantity * orderItem.UnitPrice);
        //    return Ok(total);
        //}

        //[HttpGet("DoanhThuTheoNgay")]
        //public IActionResult TongHoaDonTheoNgay([FromQuery] DateTime date)
        //{
        //    var total = context.Orders
        //    .Where(order => order.CreatedAt.Date == date.Date)
        //    .SelectMany(order => order.OrderItems)
        //    .Sum(orderItem => orderItem.Quantity * orderItem.UnitPrice);

        //    return Ok(new 
        //    { 
        //        Date = date, 
        //        TotalRevenue = total 
        //    }
        //    );
        //}

        //[HttpGet("TongHoaDonTheoNgay")]
        //public IActionResult DoanhThuTheoNgay([FromQuery] DateTime date)
        //{
        //    var total = context.Orders
        //    .Where(order => order.CreatedAt.Date == date.Date).Count();

        //    return Ok(new
        //    {
        //        Date = date,
        //        TotalRevenue = total
        //    }
        //    );
        //}

        [HttpGet("DoanhThu")]
        public IActionResult ThongKe([FromQuery] DateTime? date)
        {
            if (date.HasValue)
            {
                // Tính doanh thu theo ngày
                var dailyRevenue = context.Orders
                    .Where(order => order.CreatedAt.Date == date.Value.Date)
                    .SelectMany(order => order.OrderItems)
                    .Sum(orderItem => orderItem.Quantity * orderItem.UnitPrice);

                return Ok(new
                {
                    Date = date.Value,
                    Total = dailyRevenue
                });
            }
            else
            {
                // Tính tổng doanh thu
                decimal total = context.OrderItems.Sum(orderItem => orderItem.Quantity * orderItem.UnitPrice);
                return Ok(total);
            }
        }

        [HttpGet("TongHoaDon")]
        public IActionResult TongSanPham([FromQuery] DateTime? date)
        {
            if (date.HasValue)
            {
                var total = context.Orders
                .Where(order => order.CreatedAt.Date == date.Value.Date).Count();

                return Ok(new
                {
                    Date = date,
                    Total = total
                }
                );
            }
            else
            {
                // Tính tổng số hóa đơn
                decimal total = context.Orders.Count();
                return Ok(total);
            }
        }

        [HttpGet("TongKhachHang")]
        public IActionResult TongKhachHang([FromQuery] DateTime? date)
        {
            if (date.HasValue)
            {
                var total = context.Orders.Include(o => o.User)
                .Where(order => order.CreatedAt.Date == date.Value.Date).Count();

                return Ok(new
                {
                    Date = date,
                    Total = total
                }
                );
            }
            else
            {
                // Tính tổng sản phẩm
                IQueryable<Order> query = context.Orders.Include(o => o.User);

                decimal total = query.Count();

                return Ok(total);
            }
        }

        [HttpGet("TongSanPham")]
        public IActionResult TongSanPham()
        {
            var total = context.OrderItems.Sum(o => o.Quantity);

            return Ok(total);
        }

    }
}
