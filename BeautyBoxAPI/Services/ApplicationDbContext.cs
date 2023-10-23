using BeautyBoxAPI.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X500;

namespace BeautyBoxAPI.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PasswordReset> PasswordReset { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Promotions> Promotions { get; set;}

        public DbSet<Banner> Banner { get; set; }
    }
}
