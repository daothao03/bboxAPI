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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetUsers(int? page)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }

            int pageSize = 5;
            int totalPages = 0;

            decimal count = context.Users.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);


            var users = context.Users
                .OrderByDescending(u => u.Id)
                .Skip((int)(page - 1) * pageSize)
                .Take(pageSize)
            .ToList();

            List<UserProfileDTO> userProfiles = new List<UserProfileDTO>();
            foreach (var user in users)
            {
                var userProfileDto = new UserProfileDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                };

                userProfiles.Add(userProfileDto);
            }


            var response = new
            {
                Users = userProfiles,
                TotalPages = totalPages,
                PageSize = pageSize,
                Page = page
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            var userProfileDto = new UserProfileDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Ok(userProfileDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("UpdateUserRole/{userId}")]
        public IActionResult UpdateUserRole(int userId, [FromBody] string newRole)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Cập nhật quyền của người dùng
            user.Role = newRole;
            context.SaveChanges();

            return Ok("Quyền của người dùng đã được cập nhật.");
        }
    }
}
