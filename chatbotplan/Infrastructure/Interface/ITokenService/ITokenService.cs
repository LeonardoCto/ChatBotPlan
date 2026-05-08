using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatBotPlan.Domain.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

    ClaimsPrincipal GetMainFromExpiredToken(string token);
}