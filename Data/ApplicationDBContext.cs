using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
           

        }

         public DbSet<Stock> Stock { get; set; }
            public DbSet<Comment> Comment { get; set; }

    }
}
