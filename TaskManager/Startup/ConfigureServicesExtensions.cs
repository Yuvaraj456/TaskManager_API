using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.DatabaseContext;
using TaskManager.Filters;
using TaskManager.Identity;
using TaskManager.Service;
using TaskManager.ServiceContracts;
using TaskManager_Core.Domain.RepositoryContracts;
using TaskManager_Core.Service;
using TaskManager_Core.ServiceContracts;
using TaskManager_Infrastructure.Repository;

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

                options.Filters.Add(new AuthorizeFilter(policy)); //applies all the controllers, if not authenticated not allowed to endpoint

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

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IJwtTokenService, JwtTokenService>();





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

            // Avoid claim mapping to old ms soap namespaces. Avoid replace "role" by "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            //System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //JWT Authentication
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //if the authentication is failed
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//then validate this authentication
                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; //then validate this authentication
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateAudience = true,
                     ValidAudience = configuration["Jwt:Audience"],
                     ValidateIssuer = true,
                     ValidIssuer = configuration["Jwt:Issuer"],
                     ValidateLifetime = true, //if token expires it is treated as invalid token, then action method will not execute.
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Key"])),
                     //NameClaimType = "name",
                     //RoleClaimType ="role",                     
                 };
             });

            service.AddAuthorization(options => { });//optional 

            return service;

        }
    }
}
