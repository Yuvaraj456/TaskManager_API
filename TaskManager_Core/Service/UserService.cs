﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Identity;
using TaskManager.ServiceContracts;
using TaskManager.ViewModels;
using TaskManager_Core.Domain.Entities;
using TaskManager_Core.Domain.RepositoryContracts;
using TaskManager_Core.DTO;
using TaskManager_Core.ServiceContracts;

namespace TaskManager.Service
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository  _userRepositoty;

        public UserService()
        {
        }

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _userRepositoty = userRepository;
        } 

        public async Task<AuthenticationResponse?> Authenticate(LoginViewModel loginViewModel)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _userManager.FindByNameAsync(loginViewModel.UserName);
                //applicationUser.PasswordHash = null;
                if(applicationUser == null)
                {
                    return null;  
                }
                //applicationUser.Role = "Admin";

                if (await _userManager.IsInRoleAsync(applicationUser, "Admin"))
                {
                    applicationUser.Role = "Admin";
                }
                else if (await _userManager.IsInRoleAsync(applicationUser, "Employee"))
                {
                    applicationUser.Role = "Employee";
                }

                //signinUser
                await _signInManager.SignInAsync(applicationUser,false);

                AuthenticationResponse response  = await _jwtTokenService.CreateJwtToken(applicationUser);
                applicationUser.RefreshToken = response.RefreshToken;
                applicationUser.RefreshTokenExpirationDateTime = response.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(applicationUser);

                return response;

            }
            else
            {
                return null;
            }
            }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="signUpViewModel"></param>
       /// <returns></returns>
        public async Task<AuthenticationResponse?> Register(SignUpViewModel signUpViewModel)
        {
            if (signUpViewModel == null)
                return null;

            ApplicationUser applicationUser = new ApplicationUser()
            {                
                FirstName = signUpViewModel.PersonName.FirstName,
                LastName = signUpViewModel.PersonName.LastName,
                Email = signUpViewModel.Email,
                PhoneNumber = signUpViewModel.Mobile,
                ReceivesNewsLetter = signUpViewModel.ReceivesNewsLetter,
                CountryId = signUpViewModel.CountryId, 
                Role = "Employee",
                DateOfBirth = Convert.ToDateTime(signUpViewModel.DateOfBirth),
                Gender = signUpViewModel.Gender,
                UserName = signUpViewModel.Email,
                
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, signUpViewModel.Password);                        

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(applicationUser, "Employee");
                //signIn user
                SignInResult signInResult =  await _signInManager.PasswordSignInAsync(applicationUser,signUpViewModel.Password, isPersistent: false,lockoutOnFailure:false);

                if (signInResult.Succeeded)
                {
                    //applicationUser.PasswordHash = null;

                    AuthenticationResponse response = await _jwtTokenService.CreateJwtToken(applicationUser);


                    applicationUser.RefreshToken = response.RefreshToken;
                    applicationUser.RefreshTokenExpirationDateTime = response.RefreshTokenExpirationDateTime;

                    await _userManager.UpdateAsync(applicationUser);

                    List<Skill> skills = new List<Skill>();
                    foreach(var item in signUpViewModel.Skills)
                    {
                        Skill skill = new Skill() { 
                             Id = applicationUser.Id,
                             SkillId = item.SkillId,
                             SkillLevel = item.SkillLevel,
                             SkillName = item.SkillName,                                
                        };

                        skills.Add(skill);
                    }

                    await _userRepositoty.AddSkills(skills);

                    return response;
                }

            }               

            return null;
           
        }

        public async Task<ApplicationUser?> GetUserByEmailService(string email)
        {
            if (email == null)
                return null;

           ApplicationUser? user = await _userManager.FindByEmailAsync(email);
           
            if(user == null) 
                return null;

            return user;

        }

        public async Task<AuthenticationResponse?> GenerateNewAccessToken(TokenModel jwtToken)
        {

            ClaimsPrincipal? Principal = _jwtTokenService.GetPrincipalFromJwtToken(jwtToken.Token);

            if (Principal == null)
            {
                return null;
            }
            string? role = Principal.FindFirstValue(ClaimTypes.Role);
            string? email = Principal.FindFirstValue(ClaimTypes.Email);

            ApplicationUser? user = await _userManager.FindByEmailAsync(email??"");
            
            if (user == null || user.RefreshToken != jwtToken.RefreshToken || user.RefreshTokenExpirationDateTime <= DateTime.Now)
            {
                return null;
            }

            user.Role = role ?? "";
            AuthenticationResponse authResponse = await _jwtTokenService.CreateJwtToken(user);

            user.RefreshToken = authResponse.RefreshToken;

            user.RefreshTokenExpirationDateTime = authResponse.RefreshTokenExpirationDateTime;

            await _userManager.UpdateAsync(user);

            return authResponse;
        }

    }
}
