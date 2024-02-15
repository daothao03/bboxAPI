using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public OrdersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //get paymentmethod
        [HttpGet("paymentmethod")]
        public IActionResult GetPaymentmethod()
        {
            var paymentmethod = OrderHelper.PaymentMethods.ToList();
            return Ok(paymentmethod);
        }

        //tạo đơn hàng
        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder(OrderDTO orderDTO)
        {
            decimal totalAmount = 0;

            // kiểm tra pttt
            if (!OrderHelper.PaymentMethods.ContainsKey(orderDTO.PaymentMethod))
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
            var productDictionary = OrderHelper.GetProductDictionary(orderDTO.ProductIdentifiers);

            //tạo đơn hàng
            Order order = new Order();
            order.UserId = userId;
            order.CreatedAt = DateTime.Now;
            order.ShippingFee = OrderHelper.ShippingFee;
            order.DeliveryAddress = orderDTO.DeliveryAddress;
            order.PaymentMethod = orderDTO.PaymentMethod;
            order.PaymentStatus = OrderHelper.PaymentStatuses[0];
            order.OrderStatus = OrderHelper.OrderStatuses[0];

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
                var orderItem = new OrderItem();
                orderItem.ProductId = productId;
                orderItem.Quantity = pair.Value;
                orderItem.UnitPrice = product.Price;

                //thêm đơn hàng
                order.OrderItems.Add(orderItem);

                // tính tổng tiền cho từng sản phẩm
                totalAmount += orderItem.UnitPrice * orderItem.Quantity;
            }

            //kiểm tra giỏ hàng có sp không
            if (order.OrderItems.Count < 1)
            {
                ModelState.AddModelError("Đơn hàng", "Đơn hàng không hợp lệ");
                return BadRequest(ModelState);
            }


            // lưu đơn hàng vào csdl 
            context.Orders.Add(order);
            context.SaveChanges();


            // get rid of the object cycle
            foreach (var item in order.OrderItems)
            {
                item.Order = null;
            }

            // hide the user password
            order.User.Password = "";

            var returns = new
            {
                Order = order,
                Total = totalAmount
            };
            return Ok(returns);
        }

        //lấy ra đơn hàng, phân trang
        [Authorize]
        [HttpGet]
        public IActionResult GetOrders(string? name, string? phone, int? page)
        {
            int userId = JwtReader.GetUserId(User);
            string role = context.Users.Find(userId)?.Role ?? ""; // JwtReader.GetUserRole(User);

            IQueryable<Order> query = context.Orders.Include(o => o.User)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product);

            //search
            if(name != null)
            {
                query = query.Where(o => o.User.FirstName.Contains(name) || o.User.LastName.Contains(name));
            }

            if (phone != null)
            {
                query = query.Where(o => o.User.Phone == phone);
            }

            if (role != "admin")
            {
                query = query.Where(o => o.UserId == userId);
            }

            query = query.OrderByDescending(o => o.Id);


            // phân trang
            if (page == null || page < 1)
            {
                page = 1;
            }

            int pageSize = 5;
            int totalPages = 0;

            decimal count = query.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);

            query = query.Skip((int)(page - 1) * pageSize)
                .Take(pageSize);


            // đọc đơn hàng
            var orders = query.ToList();


            foreach (var order in orders)
            {
                // loại bỏ cảnh báo
                foreach (var item in order.OrderItems)
                {
                    item.Order = null;
                }

                order.User.Password = "";
            }


            var response = new
            {
                Orders = orders,
                TotalPages = totalPages,
                PageSize = pageSize,
                Page = page
            };

            return Ok(response);
        }

        //lấy ra đơn hàng theo id
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            int userId = JwtReader.GetUserId(User);
            string role = context.Users.Find(userId)?.Role ?? ""; // JwtReader.GetUserRole(User);

            Order? order = null;

            if (role == "admin")
            {
                order = context.Orders.Include(o => o.User)
                    .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                    .FirstOrDefault(o => o.Id == id);
            }
            else
            {
                order = context.Orders.Include(o => o.User)
                    .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                    .FirstOrDefault(o => o.Id == id && o.UserId == userId);
            }

            if (order == null)
            {
                return NotFound();
            }


            // get rid of the object cycle
            foreach (var item in order.OrderItems)
            {
                item.Order = null;
            }


            // hide the user password
            order.User.Password = "";


            return Ok(order);
        }

        //sửa đơn hàng

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, string? paymentStatus, string? orderStatus)
        {
            if (paymentStatus == null && orderStatus == null)
            {
                // we have nothing to do
                ModelState.AddModelError("Update Order", "There is nothing to update");
                return BadRequest(ModelState);
            }


            if (paymentStatus != null && !OrderHelper.PaymentStatuses.Contains(paymentStatus))
            {
                // the payment status is not valid
                ModelState.AddModelError("Payment Status", "The Payment Status is not valid");
                return BadRequest(ModelState);
            }


            if (orderStatus != null && !OrderHelper.OrderStatuses.Contains(orderStatus))
            {
                // the order status is not valid
                ModelState.AddModelError("Order Status", "The Order Status is not valid");
                return BadRequest(ModelState);
            }


            var order = context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            if (paymentStatus != null)
            {
                order.PaymentStatus = paymentStatus;
            }

            if (orderStatus != null)
            {
                order.OrderStatus = orderStatus;
            }


            context.SaveChanges();

            return Ok(order);
        }

        //xóa đơn hàng
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            context.Orders.Remove(order);
            context.SaveChanges();

            return Ok();
        }
    }
}
