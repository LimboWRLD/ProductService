
using Microsoft.EntityFrameworkCore;
using TiacPraksaP1.Models;

namespace TiacPraksaP1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
