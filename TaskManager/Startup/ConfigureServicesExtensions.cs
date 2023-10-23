using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using TaskManager.Filters;
using TaskManager.Identity;
using TaskManager.Service;
using TaskManager.ServiceContracts;

namespace TaskManager.Startup
{
    public static class ConfigureServicesExtensions
    {

        public static IServiceCollection ConfigureServices(this IServiceCollection service, IConfiguration configuration)
        {

            // Add services to the container.
            service.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                //options.Filters.Add(new AuthorizeFilter(policy)); //applies all the controllers, if not authenticated not allowed to endpoint

                options.Filters.Add(typeof(GlobalExceptionFilter));
            });

            service.AddSwaggerGen();

            //Identity
            service.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>>()//identityUser Store added here!
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, int>>();//identityRole Store added here!


            service.AddTransient<IUserService, UserService>();




            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            service.AddCors(options =>
            {
                options.AddDefaultPolicy(policybuilder =>
                {
                    policybuilder.WithOrigins(configuration.GetSection("AllowOrigins").Get<string[]>())
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            //JWT Authentication
            //service.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //if the authentication is failed
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//then validate this authentication
            //    //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; //then validate this authentication
            //})
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //         ValidateAudience = true,
            //         ValidAudience = builder.Configuration["Jwt:Audience"],
            //         ValidateIssuer = true,
            //         ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //         ValidateLifetime = true, //if token expires it is treated as invalid token, then action method will not execute.
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Key"]))

            //     };
            // });

            service.AddAuthorization(options => { });//optional 

            return service;

        }
    }
}
