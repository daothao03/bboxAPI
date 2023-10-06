using Microsoft.EntityFrameworkCore;

namespace BeautyBoxAPI.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
