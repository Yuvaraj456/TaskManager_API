using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity;
using TaskManager_Core.DTO;

namespace TaskManager_Core.ServiceContracts
{
    public interface IJwtTokenService
    {
        Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string? user);
    }
}
