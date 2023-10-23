using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X500;

namespace BeautyBoxAPI.Controllers
{
    [Authorize(Roles="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HDNController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public HDNController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
    }
}
