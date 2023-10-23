using TaskManager.Identity;
using TaskManager.ViewModels;
using TaskManager_Core.DTO;

namespace TaskManager.ServiceContracts
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);

        Task<ApplicationUser> CreateUser(RegisterDTO registerDTO);
    }
}
