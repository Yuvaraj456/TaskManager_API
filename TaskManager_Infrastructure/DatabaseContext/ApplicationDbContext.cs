using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Identity;
using TaskManager_Core.Domain.Entities;

namespace TaskManager.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ClientLocation> ClientLocations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>()
                .Property(c => c.ProjectId)
                .ValueGeneratedNever();


            builder.Entity<ClientLocation>().HasData(
                new ClientLocation() { ClientLocationId = 1, ClientLocationName = "Boston" },
                new ClientLocation() { ClientLocationId = 2, ClientLocationName = "New Delhi" },
                new ClientLocation() { ClientLocationId = 3, ClientLocationName = "New Jercy" },
                new ClientLocation() { ClientLocationId = 4, ClientLocationName = "New York" },
                new ClientLocation() { ClientLocationId = 5, ClientLocationName = "London" },
                new ClientLocation() { ClientLocationId = 6, ClientLocationName = "Tokyo" }
                );

        }

    }
}
