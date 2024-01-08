using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
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
            if (login == null)
                return BadRequest("User Name & Password is Null");

            var user = await _userService.Authenticate(login);
             
            if(user == null)
            {
                return BadRequest("User Name or Password is incorrect");
            }

            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostRegister([FromBody] SignUpViewModel signUpViewModel) 
        {
            //validation
            if (!ModelState.IsValid) 
            {
                string ErrorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return Problem(ErrorMessage);
            }          

            AuthenticationResponse result = await _userService.Register(signUpViewModel);
             
           if(result == null)
            {
                BadRequest("invalid data");
            }
          
             return Ok(result);
            
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<ApplicationUser>> GetUserByEmail([FromRoute]string email) 
       {
            ApplicationUser? user = await _userService.GetUserByEmailService(email);          

            return Ok(user);
        }
    }
}
