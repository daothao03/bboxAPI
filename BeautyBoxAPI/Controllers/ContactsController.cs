using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        //private readonly List<string> listSubjects = new List<string>()
        //{
        //    "Order Status", "Refund Request", "Job Application", "Other"
        //};

        public ContactsController(ApplicationDbContext context)
        {
            this._context = context;
        }


        //Get all subjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var listSubjects = _context.Subjects.ToList();
            return Ok(listSubjects);
        }


        //Get all contacts + Pagination
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetContacts(int? page) //page: trang hiện tại
        {
            if (page == null || page < 1)
            {
                page = 1;
            }

            int pageSize = 10;
            int totalPages = 0; //hiển thị các nút phân trang

            decimal countContacts = _context.Contacts.Count();
            totalPages = (int) Math.Ceiling(countContacts / pageSize); //Số trang 

            var contacts = _context.Contacts
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

            return Ok(contact);
        }


        //Update Contact
        [HttpPut("id")]
        //public IActionResult UpdateContact(int id, ContactsDTO contactsDTO)
        //{
        //    var subject = _context.Subjects.Find(contactsDTO.SubjectID);
        //    if (subject == null)
        //    //if (!listSubjects.Contains(contactsDTO.Subject))
        //    {
        //        ModelState.AddModelError("Subjects", "Please select a valid subjects");
        //        return BadRequest(ModelState);
        //    }

        //    var contact = _context.Contacts.Find(id);
        //    if(contact == null)
        //    {
        //        return NotFound();
        //    }    

        //    contact.FirstName = contactsDTO.FirstName;
        //    contact.LastName = contactsDTO.LastName;
        //    contact.Email = contactsDTO.Email;
        //    contact.Phone = contactsDTO.Phone ?? "";
        //    //contact.Subject = contactsDTO.Subject;
        //    contact.Subject = subject;
        //    contact.Message = contactsDTO.Message;

        //    _context.SaveChanges();
        //    return Ok();
        //}


        //Delete Contact
        [Authorize(Roles = "admin")]
        [HttpDelete("id")]
        public IActionResult DeleteContact(int id)
        {
            //Method 1: Use 2 query Query 1: Find the contact you want to delete
            //                      Query 2: Delete contact
            /*
            var contact = _context.Contacts.Find(id);
            if(contact == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return Ok();
            */

            //Method 2: Optimize - Use 1 query to delete contact
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
