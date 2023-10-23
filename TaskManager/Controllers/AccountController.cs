using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Identity;
using TaskManager.ServiceContracts;
using TaskManager.ViewModels;
using TaskManager_Core.DTO;

namespace TaskManager_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate([FromBody]LoginViewModel login)
        {
            var user = await _userService.Authenticate(login);

            if(user == null)
            {
                return BadRequest("User Name or Password is incorrect");
            }

            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUser>> PostRegister([FromBody] RegisterDTO registerDTO)
        {
            //validation
            if (!ModelState.IsValid)
            {
                string ErrorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return Problem(ErrorMessage);
            }


            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,                
                PhoneNumber = registerDTO.PhoneNumber,
            };

            ApplicationUser result = await _userService.CreateUser(registerDTO);

           if(result== null)
            {
                BadRequest("invalid UserName and Password");
            }
          
             return Ok(applicationUser);
            
        }
    }
}
