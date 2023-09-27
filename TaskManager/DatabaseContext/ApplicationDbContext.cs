using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions dbContext) : base(dbContext)
        {
            
        }

        public DbSet<Project> Projects { get; set; }


    }
}
