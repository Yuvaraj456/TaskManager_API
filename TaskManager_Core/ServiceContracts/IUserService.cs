using TaskManager.Identity;
using TaskManager.ViewModels;
using TaskManager_Core.DTO;

namespace TaskManager.ServiceContracts
{
    public interface IUserService
    {
        Task<AuthenticationResponse?> Authenticate(LoginViewModel loginViewModel);

        Task<AuthenticationResponse> Register(SignUpViewModel signUpViewModel);

        Task<ApplicationUser?> GetUserByEmailService(string email);
    }
}
