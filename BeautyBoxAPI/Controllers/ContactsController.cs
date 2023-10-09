using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Add Pagination


        //Get all subjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var listSubjects = _context.Subjects.ToList();
            return Ok(listSubjects);
        }

        //Get all contacts
        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = _context.Contacts.Include(contact => contact.Subject).ToList();
            return Ok(contacts);
        }

        //Get contacts by ID
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
        public IActionResult UpdateContact(int id, ContactsDTO contactsDTO)
        {
            var subject = _context.Subjects.Find(contactsDTO.SubjectID);
            if (subject == null)
            //if (!listSubjects.Contains(contactsDTO.Subject))
            {
                ModelState.AddModelError("Subjects", "Please select a valid subjects");
                return BadRequest(ModelState);
            }

            var contact = _context.Contacts.Find(id);
            if(contact == null)
            {
                return NotFound();
            }    

            contact.FirstName = contactsDTO.FirstName;
            contact.LastName = contactsDTO.LastName;
            contact.Email = contactsDTO.Email;
            contact.Phone = contactsDTO.Phone ?? "";
            //contact.Subject = contactsDTO.Subject;
            contact.Subject = subject;
            contact.Message = contactsDTO.Message;

            _context.SaveChanges();
            return Ok();
        }

        //Delete Contact
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
