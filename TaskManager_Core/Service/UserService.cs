
using Microsoft.AspNetCore.Identity;
using TaskManager.Identity;
using TaskManager.ServiceContracts;
using TaskManager.ViewModels;
using TaskManager_Core.DTO;

namespace TaskManager.Service
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService()
        {
        }

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _userManager.FindByNameAsync(loginViewModel.UserName);
                applicationUser.PasswordHash = null;
                return applicationUser;

            }
            else
            {
                return null;
            }
        }

        public async Task<ApplicationUser> CreateUser(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return null;

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, registerDTO.Password);

            if (result.Succeeded)
            {
                //signIn user
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);

                bool roleExist = await _roleManager.RoleExistsAsync("Employee");

                if(!roleExist)
                {
                    ApplicationRole applicationRole = new ApplicationRole()
                    {
                        Name = "Employee"
                    };
                    IdentityResult createRole = await _roleManager.CreateAsync(applicationRole);
                }

                IdentityResult role = await _userManager.AddToRoleAsync(applicationUser, "Employee");
                return applicationUser;
            }


            return null;
           
        }
    }
}
