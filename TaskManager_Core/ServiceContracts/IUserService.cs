using System.Security.Claims;
using TaskManager.Identity;
using TaskManager.ViewModels;
using TaskManager_Core.Domain.Entities;
using TaskManager_Core.DTO;

namespace TaskManager.ServiceContracts
{
    public interface IUserService
    {
        Task<AuthenticationResponse?> Authenticate(LoginViewModel loginViewModel);

        Task<AuthenticationResponse?> Register(SignUpViewModel signUpViewModel);

        Task<ApplicationUser?> GetUserByEmailService(string email);

        Task<AuthenticationResponse?> GenerateNewAccessToken(TokenModel jwtToken);
    }
}
