using Microsoft.AspNetCore.Identity;
using Yam.API.Model.Dtos.Account;
using Yam.API.Model.Dtos.Account.Request;
using Yam.API.Model.Dtos.Account.Response;

namespace Yam.API.Services;

public interface IJwtService
{
    Task<AuthResult> GenerateToken(IdentityUser user);
    Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest);
}
