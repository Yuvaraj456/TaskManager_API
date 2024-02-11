using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskManager.Identity;
using TaskManager_Core.Domain.Entities;
using Model = TaskManager_Core.Domain.Entities;
namespace TaskManager.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ClientLocation> ClientLocations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<TaskManager_Core.Domain.Entities.TaskStatus> TaskStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>()
                .Property(c => c.ProjectId)
                .ValueGeneratedNever();

            builder.Entity<TaskPriority>().HasData(
                new TaskPriority() { TaskPriorityId = 1, TaskPriorityName = "Urgent" },
                new TaskPriority() { TaskPriorityId = 2, TaskPriorityName = "Normal" },
                new TaskPriority() { TaskPriorityId = 3, TaskPriorityName = "Below Normal" },
                new TaskPriority() { TaskPriorityId = 4, TaskPriorityName = "Low" }
                );

            builder.Entity<Model.TaskStatus>().HasData(
               new Model.TaskStatus() { TaskStatusId = 1, TaskStatusName = "Holding" }, //Tasks that need to be documented still
               new Model.TaskStatus() { TaskStatusId = 2, TaskStatusName = "Prioritized" },//Tasks that are placed in priority order; so need to start ASAP
               new Model.TaskStatus() { TaskStatusId = 3, TaskStatusName = "Started" },//Tasks that are currently working
               new Model.TaskStatus() { TaskStatusId = 4, TaskStatusName = "Finished" },//Tasks that are finished working
               new Model.TaskStatus() { TaskStatusId = 5, TaskStatusName = "Reverted" }//Tasks that are reverted back, with comments or issues
               );

            builder.Entity<ClientLocation>().HasData(
                new ClientLocation() { ClientLocationId = 1, ClientLocationName = "Boston" },
                new ClientLocation() { ClientLocationId = 2, ClientLocationName = "New Delhi" },
                new ClientLocation() { ClientLocationId = 3, ClientLocationName = "New Jercy" },
                new ClientLocation() { ClientLocationId = 4, ClientLocationName = "New York" },
                new ClientLocation() { ClientLocationId = 5, ClientLocationName = "London" },
                new ClientLocation() { ClientLocationId = 6, ClientLocationName = "Tokyo" }
            );


            builder.Entity<Country>().HasData(
                new Country() { CountryId = 1, CountryName = "China" },
                new Country() { CountryId = 2, CountryName = "United States" },
                new Country() { CountryId = 3, CountryName = "Indonesia" },
                new Country() { CountryId = 4, CountryName = "Brazil" },
                new Country() { CountryId = 5, CountryName = "Pakistan" },
                new Country() { CountryId = 6, CountryName = "Nigeria" },
                new Country() { CountryId = 7, CountryName = "Bangladesh" },
                new Country() { CountryId = 8, CountryName = "Russia" },
                new Country() { CountryId = 9, CountryName = "Japan" },
                new Country() { CountryId = 10, CountryName = "Mexico" },
                new Country() { CountryId = 11, CountryName = "Philippines" },
                new Country() { CountryId = 12, CountryName = "Vietnam" },
                new Country() { CountryId = 13, CountryName = "Ethiopia" },
                new Country() { CountryId = 14, CountryName = "Egypt" },
                new Country() { CountryId = 15, CountryName = "Germany" },
                new Country() { CountryId = 16, CountryName = "Iran" },
                new Country() { CountryId = 17, CountryName = "Turkey" },
                new Country() { CountryId = 18, CountryName = "Democratic Republic of the Congo" },
                new Country() { CountryId = 19, CountryName = "Thailand" },
                new Country() { CountryId = 20, CountryName = "France" },
                new Country() { CountryId = 21, CountryName = "United Kingdom" },
                new Country() { CountryId = 22, CountryName = "Italy" },
                new Country() { CountryId = 23, CountryName = "South Africa" },
                new Country() { CountryId = 24, CountryName = "South Korea" },
                new Country() { CountryId = 25, CountryName = "Myanmar" },
                new Country() { CountryId = 26, CountryName = "Spain" },
                new Country() { CountryId = 27, CountryName = "Colombia" },
                new Country() { CountryId = 28, CountryName = "Ukraine" },
                new Country() { CountryId = 29, CountryName = "Tanzania" },
                new Country() { CountryId = 30, CountryName = "Argentina" },
                new Country() { CountryId = 31, CountryName = "Kenya" },
                new Country() { CountryId = 32, CountryName = "Poland" },
                new Country() { CountryId = 33, CountryName = "Algeria" },
                new Country() { CountryId = 34, CountryName = "Canada" },
                new Country() { CountryId = 35, CountryName = "Uganda" },
                new Country() { CountryId = 36, CountryName = "Iraq" },
                new Country() { CountryId = 37, CountryName = "Morocco" },
                new Country() { CountryId = 38, CountryName = "Sudan" },
                new Country() { CountryId = 39, CountryName = "Peru" },
                new Country() { CountryId = 40, CountryName = "Malaysia" },
                new Country() { CountryId = 41, CountryName = "Uzbekistan" },
                new Country() { CountryId = 42, CountryName = "Saudi Arabia" },
                new Country() { CountryId = 43, CountryName = "Venezuela" },
                new Country() { CountryId = 44, CountryName = "Nepal" },
                new Country() { CountryId = 45, CountryName = "Afghanistan" },
                new Country() { CountryId = 46, CountryName = "Ghana" },
                new Country() { CountryId = 47, CountryName = "Yemen" },
                new Country() { CountryId = 48, CountryName = "North Korea" },
                new Country() { CountryId = 49, CountryName = "Mozambique" },
                new Country() { CountryId = 50, CountryName = "Taiwan" },
                new Country() { CountryId = 51, CountryName = "Australia" },
                new Country() { CountryId = 52, CountryName = "Syria" },
                new Country() { CountryId = 53, CountryName = "Ivory Coast" },
                new Country() { CountryId = 54, CountryName = "Madagascar" },
                new Country() { CountryId = 55, CountryName = "Angola" },
                new Country() { CountryId = 56, CountryName = "Sri Lanka" },
                new Country() { CountryId = 57, CountryName = "Cameroon" },
                new Country() { CountryId = 58, CountryName = "Romania" },
                new Country() { CountryId = 59, CountryName = "Kazakhstan" },
                new Country() { CountryId = 60, CountryName = "Netherlands" },
                new Country() { CountryId = 61, CountryName = "Chile" },
                new Country() { CountryId = 62, CountryName = "Niger" },
                new Country() { CountryId = 63, CountryName = "Burkina Faso" },
                new Country() { CountryId = 64, CountryName = "Ecuador" },
                new Country() { CountryId = 65, CountryName = "Guatemala" },
                new Country() { CountryId = 66, CountryName = "Mali" },
                new Country() { CountryId = 67, CountryName = "Malawi" },
                new Country() { CountryId = 68, CountryName = "Senegal" },
                new Country() { CountryId = 69, CountryName = "Cambodia" },
                new Country() { CountryId = 70, CountryName = "Zambia" },
                new Country() { CountryId = 71, CountryName = "Zimbabwe" },
                new Country() { CountryId = 72, CountryName = "Chad" },
                new Country() { CountryId = 73, CountryName = "Cuba" },
                new Country() { CountryId = 74, CountryName = "Belgium" },
                new Country() { CountryId = 75, CountryName = "Guinea" },
                new Country() { CountryId = 76, CountryName = "Greece" },
                new Country() { CountryId = 77, CountryName = "Tunisia" },
                new Country() { CountryId = 78, CountryName = "Portugal" },
                new Country() { CountryId = 79, CountryName = "Rwanda" },
                new Country() { CountryId = 80, CountryName = "Czech Republic" },
                new Country() { CountryId = 81, CountryName = "Haiti" },
                new Country() { CountryId = 82, CountryName = "Bolivia" },
                new Country() { CountryId = 83, CountryName = "Somalia" },
                new Country() { CountryId = 84, CountryName = "Hungary" },
                new Country() { CountryId = 85, CountryName = "Benin" },
                new Country() { CountryId = 86, CountryName = "Sweden" },
                new Country() { CountryId = 87, CountryName = "Belarus" },
                new Country() { CountryId = 88, CountryName = "Dominican Republic" },
                new Country() { CountryId = 89, CountryName = "Azerbaijan" },
                new Country() { CountryId = 90, CountryName = "Austria" },
                new Country() { CountryId = 91, CountryName = "Honduras" },
                new Country() { CountryId = 92, CountryName = "United Arab Emirates" },
                new Country() { CountryId = 93, CountryName = "South Sudan" },
                new Country() { CountryId = 94, CountryName = "Burundi" },
                new Country() { CountryId = 95, CountryName = "Switzerland" },
                new Country() { CountryId = 96, CountryName = "Israel" },
                new Country() { CountryId = 97, CountryName = "Tajikistan" },
                new Country() { CountryId = 98, CountryName = "Bulgaria" },
                new Country() { CountryId = 99, CountryName = "Serbia" },
                new Country() { CountryId = 100, CountryName = "Papua New Guinea" },
                new Country() { CountryId = 101, CountryName = "Paraguay" },
                new Country() { CountryId = 102, CountryName = "Laos" },
                new Country() { CountryId = 103, CountryName = "Libya" },
                new Country() { CountryId = 104, CountryName = "Jordan" },
                new Country() { CountryId = 105, CountryName = "Sierra Leone" },
                new Country() { CountryId = 106, CountryName = "Togo" },
                new Country() { CountryId = 107, CountryName = "El Salvador" },
                new Country() { CountryId = 108, CountryName = "Nicaragua" },
                new Country() { CountryId = 109, CountryName = "Eritrea" },
                new Country() { CountryId = 110, CountryName = "Denmark" },
                new Country() { CountryId = 111, CountryName = "Kyrgyzstan" },
                new Country() { CountryId = 112, CountryName = "Slovakia" },
                new Country() { CountryId = 113, CountryName = "Finland" },
                new Country() { CountryId = 114, CountryName = "Singapore" },
                new Country() { CountryId = 115, CountryName = "Turkmenistan" },
                new Country() { CountryId = 116, CountryName = "Norway" },
                new Country() { CountryId = 117, CountryName = "Costa Rica" },
                new Country() { CountryId = 118, CountryName = "Central African Republic" },
                new Country() { CountryId = 119, CountryName = "Ireland" },
                new Country() { CountryId = 120, CountryName = "Georgia" },
                new Country() { CountryId = 121, CountryName = "New Zealand" },
                new Country() { CountryId = 122, CountryName = "Republic of the Congo" },
                new Country() { CountryId = 123, CountryName = "Lebanon" },
                new Country() { CountryId = 124, CountryName = "Palestine" },
                new Country() { CountryId = 125, CountryName = "Croatia" },
                new Country() { CountryId = 126, CountryName = "Bosnia and Herzegovina" },
                new Country() { CountryId = 127, CountryName = "Kuwait" },
                new Country() { CountryId = 128, CountryName = "Moldova" },
                new Country() { CountryId = 129, CountryName = "Liberia" },
                new Country() { CountryId = 130, CountryName = "Mauritania" },
                new Country() { CountryId = 131, CountryName = "Panama" },
                new Country() { CountryId = 132, CountryName = "Uruguay" },
                new Country() { CountryId = 133, CountryName = "Armenia" },
                new Country() { CountryId = 134, CountryName = "Lithuania" },
                new Country() { CountryId = 135, CountryName = "Albania" },
                new Country() { CountryId = 136, CountryName = "Oman" },
                new Country() { CountryId = 137, CountryName = "Mongolia" },
                new Country() { CountryId = 138, CountryName = "Jamaica" },
                new Country() { CountryId = 139, CountryName = "Lesotho" },
                new Country() { CountryId = 140, CountryName = "Namibia" },
                new Country() { CountryId = 141, CountryName = "Macedonia" },
                new Country() { CountryId = 142, CountryName = "Slovenia" },
                new Country() { CountryId = 143, CountryName = "Latvia" },
                new Country() { CountryId = 144, CountryName = "Botswana" },
                new Country() { CountryId = 145, CountryName = "Qatar" },
                new Country() { CountryId = 146, CountryName = "Gambia" },
                new Country() { CountryId = 147, CountryName = "Gabon" },
                new Country() { CountryId = 148, CountryName = "Guinea-Bissau" },
                new Country() { CountryId = 149, CountryName = "Trinidad and Tobago" },
                new Country() { CountryId = 150, CountryName = "Estonia" },
                new Country() { CountryId = 151, CountryName = "Mauritius" },
                new Country() { CountryId = 152, CountryName = "Swaziland" },
                new Country() { CountryId = 153, CountryName = "Bahrain" },
                new Country() { CountryId = 154, CountryName = "Timor-Leste" },
                new Country() { CountryId = 155, CountryName = "Cyprus" },
                new Country() { CountryId = 156, CountryName = "Fiji" },
                new Country() { CountryId = 157, CountryName = "Djibouti" },
                new Country() { CountryId = 158, CountryName = "Guyana" },
                new Country() { CountryId = 159, CountryName = "Equatorial Guinea" },
                new Country() { CountryId = 160, CountryName = "Bhutan" },
                new Country() { CountryId = 161, CountryName = "Comoros" },
                new Country() { CountryId = 162, CountryName = "Montenegro" },
                new Country() { CountryId = 163, CountryName = "Western Sahara" },
                new Country() { CountryId = 164, CountryName = "Suriname" },
                new Country() { CountryId = 165, CountryName = "Luxembourg" },
                new Country() { CountryId = 166, CountryName = "Solomon Islands" },
                new Country() { CountryId = 167, CountryName = "Cape Verde" },
                new Country() { CountryId = 168, CountryName = "Malta" },
                new Country() { CountryId = 169, CountryName = "Brunei" },
                new Country() { CountryId = 170, CountryName = "Bahamas" },
                new Country() { CountryId = 171, CountryName = "Maldives" },
                new Country() { CountryId = 172, CountryName = "Iceland" },
                new Country() { CountryId = 173, CountryName = "Belize" },
                new Country() { CountryId = 174, CountryName = "Barbados" },
                new Country() { CountryId = 175, CountryName = "Vanuatu" },
                new Country() { CountryId = 176, CountryName = "Samoa" },
                new Country() { CountryId = 177, CountryName = "Saint Lucia" },
                new Country() { CountryId = 178, CountryName = "Kiribati" },
                new Country() { CountryId = 179, CountryName = "Grenada" },
                new Country() { CountryId = 180, CountryName = "Tonga" },
                new Country() { CountryId = 181, CountryName = "Federated States of Micronesia" },
                new Country() { CountryId = 182, CountryName = "Saint Vincent and the Grenadines" },
                new Country() { CountryId = 183, CountryName = "Seychelles" },
                new Country() { CountryId = 184, CountryName = "Antigua and Barbuda" },
                new Country() { CountryId = 185, CountryName = "Andorra" },
                new Country() { CountryId = 186, CountryName = "Dominica" },
                new Country() { CountryId = 187, CountryName = "Liechtenstein" },
                new Country() { CountryId = 188, CountryName = "Monaco" },
                new Country() { CountryId = 189, CountryName = "San Marino" },
                new Country() { CountryId = 190, CountryName = "Palau" },
                new Country() { CountryId = 191, CountryName = "Tuvalu" },
                new Country() { CountryId = 192, CountryName = "Nauru" },
                new Country() { CountryId = 193, CountryName = "Vatican City" }
            );

        }

    }
}
