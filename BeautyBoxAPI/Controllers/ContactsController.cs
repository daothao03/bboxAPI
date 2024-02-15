using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using System.Net.Mail;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;
        private readonly string senderEmail;
        private readonly string senderName;

        public ContactsController(ApplicationDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this.configuration = configuration;
            this.senderEmail = configuration["BrevoApi: SenderEmail"]!;
            this.senderName = configuration["BrevoApi: SenderName"]!;
        }


        //Get all subjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var listSubjects = _context.Subjects.ToList();
            return Ok(listSubjects);
        }

        //Create Subjects 
        [Authorize(Roles = "admin")]
        [HttpPost("createSubjects")]
        public IActionResult CreateSubject(SubjectsDTO subjectsDTO)
        {
            Subject subject = new Subject()
            {
                Name = subjectsDTO.Name
            };

            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return Ok(subject);
        }


        //Get all contacts + Pagination
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetContacts(int? page, string? name, string? phone, string? email) //page: trang hiện tại
        {
            IQueryable<Contact> query = _context.Contacts;

            //search 
            if(name != null)
            {
                query = query.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name));
            }

            if (phone != null)
            {
                query = query.Where(c => c.Phone == phone);
            }

            if (email != null)
            {
                query = query.Where(c => c.Email.Contains(email));
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            int pageSize = 10;
            int totalPages = 0; //hiển thị các nút phân trang

            decimal countContacts =query.Count();
            totalPages = (int) Math.Ceiling(countContacts / pageSize); //Số trang 

            var contacts = query
                .Include(contact => contact.Subject)
                .OrderByDescending(contact => contact.ID) //Sắp xếp liên hệ mới nhất - cũ nhất
                .Skip((int) (page - 1) * pageSize)//loại bỏ liên hệ nhất định để gọi đến trang được yêu cầu
                .Take(pageSize) //Lấy liên hệ tại trang được yêu cầu
                .ToList();

            // Vì vậy, để cho phép ứng dụng giao diện người dùng hiển thị các nút phân trang, chúng tôi sẽ không chỉ trả về

            //địa chỉ liên hệ được yêu cầu, thay vào đó chúng tôi cần trả về địa chỉ liên hệ được yêu cầu, kích thước trang, tổng số

            //của các trang và cả trang được yêu cầu.

            var returns = new
            {
                Contacts = contacts,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize
            };
            return Ok(returns);
        }


        //Get contacts by ID
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            //var contact = _context.Contacts.Find(id);
            var contact = _context.Contacts
                .Include(contact => contact.Subject) //lấy dữ liệu từ bảng Subjects theo ID 
                .FirstOrDefault(contact => contact.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }


        //Create Contact
        [HttpPost]
        public IActionResult CreateContact(ContactsDTO contactsDTO) 
        {
            //User submit a contact form, front end application will show a list of accecptable subjects
            //User submit subject not valid -> server send an error message

            //lấy dữ liệu từ bảng Subjects theo ID 
            var subject = _context.Subjects.Find(contactsDTO.SubjectID);
            if (subject == null)
            //if (!listSubjects.Contains(contactsDTO.Subject))
            {
                ModelState.AddModelError("Subjects", "Please select a valid subjects");
                return BadRequest(ModelState);
            }

            Contact contact = new Contact()
            {
                FirstName = contactsDTO.FirstName,
                LastName = contactsDTO.LastName,
                Email = contactsDTO.Email,
                Phone = contactsDTO.Phone ?? "",
                //Subject = contactsDTO.Subject,
                Subject = subject,
                Message = contactsDTO.Message
            };

            _context.Contacts.Add(contact);
            _context.SaveChanges();

            // send confirmation email
            string emailSubject = "Contact Confirmation";
            string username = contactsDTO.FirstName + " " + contactsDTO.LastName;
            string emailMessage = "Dear " + username + "\n" +
                "We received you message. Thank you for contacting us.\n" +
                "Our team will contact you very soon.\n" +
                "Best Regards\n\n" +
                "Your Message:\n" + contactsDTO.Message;

            //emailSender.SendEmail(emailSubject, contact.Email, username, emailMessage).Wait();
            EmailSender.SendEmail(senderEmail, senderName, contact.Email, username, emailSubject, emailMessage);

            return Ok(contact);
        }

        //Delete Contact
        [Authorize(Roles = "admin")]
        [HttpDelete("id")] 
        public IActionResult DeleteContact(int id)
        {

            //
            // Optimize - Use 1 query to delete contact
            try
            {
                var contact = new Contact()
                {
                    ID = id,
                    Subject = new Subject()
                };
                _context.Contacts.Remove(contact);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok();

        }
    }
}
