using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Infrastructure;
using Microsoft.Extensions.Options;

namespace ChatBotPlan.Application;

public class AuthUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _hasher;
    private readonly IUserValidator _userValidator;
    private readonly ITokenService _tokenService;
    private readonly TokenSettings _settings;


    public AuthUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserValidator userValidator, ITokenService tokenService, IOptions<TokenSettings> tokenSettings)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hasher = passwordHasher;
        _userValidator = userValidator;
        _tokenService = tokenService;
        _settings = tokenSettings.Value;
    }

    public async Task<LoginResponseDTO> ExecuteAsync(LoginRequestDTO request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null || !_hasher.Verify(request.Password, user.PassWord))
            throw new DomainException("Invalid email or password.");

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };

        var token = _tokenService.GenerateAccessToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenValidityInMinutes = _settings.RefreshTokenValidityMinutes;
        user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes));

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(ct);

        return new LoginResponseDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo,
            RefreshToken = refreshToken
        };

    }

    public async Task<TokenDTO> RefreshTokenAsync(TokenDTO tokenDto, CancellationToken ct)
    {
        if (tokenDto is null)
            throw new ArgumentNullException(nameof(tokenDto));

        string? accessToken = tokenDto.AccessToken ?? throw new ArgumentNullException(nameof(tokenDto));
        string? refreshToken = tokenDto.RefreshToken ?? throw new ArgumentNullException(nameof(tokenDto));

        var claims = _tokenService.GetMainFromExpiredToken(accessToken);

        if (claims is null)
            throw new ArgumentException("Invalid access token/ Refresh token.");

        string email = claims.FindFirstValue(ClaimTypes.Email) ?? throw new ArgumentException("Invalid claims in access token.");

        var user = await _userRepository.GetByEmailAsync(email, ct);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new ArgumentException("Invalid access token/ Refresh token.");

        var newAccessToken = _tokenService.GenerateAccessToken(claims.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.SetRefreshToken(newRefreshToken, DateTime.UtcNow.AddMinutes(_settings.RefreshTokenValidityMinutes));

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(ct);

        return new TokenDTO
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken
        };
    }

    public async Task RevokeTokenAsync(RevokeTokenDTO request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null)
            throw new ArgumentException("User not found.");

        user.SetRefreshToken(null!, DateTime.UtcNow);

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(ct);
    }
}