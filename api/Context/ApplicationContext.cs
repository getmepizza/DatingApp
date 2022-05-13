using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> AppUser { get; set; }
    }
}