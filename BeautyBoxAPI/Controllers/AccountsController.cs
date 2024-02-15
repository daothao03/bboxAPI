using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly string senderEmail;
        private readonly string senderName;

        public AccountsController(IConfiguration configuration, ApplicationDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
            this.senderEmail = configuration["BrevoApi: SenderEmail"]!;
            this.senderName = configuration["BrevoApi: SenderName"]!;
        }

        //Đăng ký người dùng
        [HttpPost("Register")]
        public IActionResult Register(UserDTO userDto)
        {
            // kiểm tra email đã tồn tại chưa
            var emailCount = context.Users.Count(u => u.Email == userDto.Email);
            if (emailCount > 0)
            {
                ModelState.AddModelError("Email", "This Email address is already used");
                return BadRequest(ModelState);
            }


            // mã hóa mật khẩu
            var passwordHasher = new PasswordHasher<User>();
            var encryptedPassword = passwordHasher.HashPassword(new User(), userDto.Password);


            // tạo tài khoản dựa trên mật khẩu được mã hóa 
            User user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone ?? "",
                Address = userDto.Address,
                Password = encryptedPassword,
                Role = "client",
                CreatedAt = DateTime.Now
            };

            context.Users.Add(user);
            context.SaveChanges();

            //tạo token trả lại cho người dùng
            var jwt = CreateJWT(user);

            UserProfileDTO userProfileDto = new UserProfileDTO()
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

            var response = new
            {
                Token = jwt,
                User = userProfileDto
            };

            return Ok(response);
        }
         
        //Phương thức xác thực người dùng
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("Error", "Email or Password not valid");
                return BadRequest(ModelState);
            }


            // xác thực mật khẩu
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(new User(), user.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password", "Wrong Password");
                return BadRequest(ModelState);
            }


            var jwt = CreateJWT(user);

            UserProfileDTO userProfileDto = new UserProfileDTO()
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

            var response = new
            {
                Token = jwt,
                User = userProfileDto
            };


            return Ok(response);
        }


        //Người dùng yêu cầu reset password
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            // kiểm tra người dùng đã yêu cầu đặt lại mật khẩu chưa
            var oldPwdReset = context.PasswordReset.FirstOrDefault(r => r.Email == email);
            if (oldPwdReset != null)
            {
                // xóa yêu cầu
                context.Remove(oldPwdReset);
            }

            // create Password Reset Token
            string token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString();

            var pwdReset = new PasswordReset()
            {
                Email = email,
                Token = token,
                CreatedAt = DateTime.Now
            };

            context.PasswordReset.Add(pwdReset);
            context.SaveChanges();


            // gửi mã token reset password qua email cho người dùng
            string emailSubject = "Password Reset" ?? "";
            string username = user.FirstName + " " + user.LastName;
            string emailMessage = "Dear " + username + "\n" +
                "We received your password reset request.\n" +
                "Please copy the following token and paste it in the Password Reset Form:\n" +
                token + "\n\n" +
                "Best Regards\n";


            EmailSender.SendEmail(senderEmail, senderName, email, username, emailSubject, emailMessage);
            //emailSender.SendEmail(emailSubject, email, username, emailMessage).Wait();

            return Ok();
        }


        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string token, string password)
        {
            var pwdReset = context.PasswordReset.FirstOrDefault(r => r.Token == token);
            if (pwdReset == null)
            {
                ModelState.AddModelError("Token", "Wrong or Expired Token");
                return BadRequest(ModelState);
            }

            var user = context.Users.FirstOrDefault(u => u.Email == pwdReset.Email);
            if (user == null)
            {
                ModelState.AddModelError("Token", "Wrong or Expired Token");
                return BadRequest(ModelState);
            }

            // mã hóa mật khẩu
            var passwordHasher = new PasswordHasher<User>();
            string encryptedPassword = passwordHasher.HashPassword(new User(), password);


            // lưu mật khẩu mã hóa vào csdl
            user.Password = encryptedPassword;


            // xóa mã token
            context.PasswordReset.Remove(pwdReset);


            context.SaveChanges();

            return Ok();
        }



        //Methods cho phép người dùng xem profile của mình
        [Authorize]
        [HttpGet("Profile")]
        public IActionResult GetProfile()
        {
            int id = JwtReader.GetUserId(User);

            var user = context.Users.Find(id);
            if (user == null)
            {
                return Unauthorized();
            }

            var userProfileDTO = new UserProfileDTO()
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

            return Ok(userProfileDTO);
        }

        [Authorize]
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile(UserProfileUpdateDTO userProfileUpdateDto)
        {
            int id = JwtReader.GetUserId(User);

            var user = context.Users.Find(id);
            if (user == null)
            {
                return Unauthorized();
            }

            // update the user profile
            user.FirstName = userProfileUpdateDto.FirstName;
            user.LastName = userProfileUpdateDto.LastName;
            user.Email = userProfileUpdateDto.Email;
            user.Phone = userProfileUpdateDto.Phone ?? "";
            user.Address = userProfileUpdateDto.Address;

            context.SaveChanges();

            //Trả lại thông tin cho người dùng
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



        [Authorize]
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword([Required, MinLength(8), MaxLength(100)] string password)
        {
            int id = JwtReader.GetUserId(User);

            var user = context.Users.Find(id);
            if (user == null)
            {
                return Unauthorized();
            }


            // mã hóa mật khẩu
            var passwordHasher = new PasswordHasher<User>();
            string encryptedPassword = passwordHasher.HashPassword(new User(), password);


            // update password
            user.Password = encryptedPassword;

            context.SaveChanges();

            return Ok();
        }

        //Jwt
        private string CreateJWT(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id" , "" +  user.Id),
                new Claim("role", user.Role)
            };

            // đọc khóa bí mật trong appinstall
            string keys = configuration["JwtSettings:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keys));//tạo đối tượng thuộc khóa bảo mật

            //thuật toán để tạo chữ ký -> tạo biến chọn thuật toán xác thực
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); //HmacSha512 = 1 khóa bí mật + DL đầu vào

            //tạo mã thông báo
            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            
            //chuyển token về kiểu chuỗi
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }

    }
}
