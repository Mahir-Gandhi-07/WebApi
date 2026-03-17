using Microsoft.EntityFrameworkCore;
using DTOs_AutoMapper.Models;

namespace DTOs_AutoMapper.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
