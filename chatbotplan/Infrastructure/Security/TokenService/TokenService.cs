using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ChatBotPlan.Application;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatBotPlan.Infrastructure;

public class TokenService : ITokenService
{
    private readonly TokenSettings _settings;

    public TokenService(IOptions<TokenSettings> settings)
    {
        _settings = settings.Value;
    }
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {

        var privateKey = Encoding.UTF8.GetBytes(_settings.SecretKey);

        var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_settings.TokenValidityMinutes),
            Audience = _settings.ValidAudience,
            Issuer = _settings.ValidIssuer,
            SigningCredentials = signinCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[128];

        using var randomNumberGenenerator = RandomNumberGenerator.Create();
        randomNumberGenenerator.GetBytes(secureRandomBytes);

        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        return refreshToken;
    }

    public ClaimsPrincipal GetMainFromExpiredToken(string token)
    {
        var secretKey = _settings.SecretKey;

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var main = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return main;
    }
}