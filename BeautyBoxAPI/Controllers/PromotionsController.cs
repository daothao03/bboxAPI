using AutoMapper;
using BeautyBoxAPI.Models;
using BeautyBoxAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PromotionsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreatePromotion (PromotionsDTO promotionsDTO)
        {
            

            return Ok();
        }
    }
}
