
using ChatBotPlan.Application;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Infrastructure;
using ChatBotPlan.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ChatBotPlan.API.Controllers;

[ApiController]
[Route("api/[auth]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IOptions<TokenSettings> _tokenSettings;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;
    private readonly AuthUserUseCase _authUserCase;

    public AuthController(AuthUserUseCase authUserCase, ITokenService tokenService, IOptions<TokenSettings> tokenSettings, IUserRepository userRepository, IPasswordHasher hasher)
    {
        _tokenService = tokenService;
        _tokenSettings = tokenSettings;
        _userRepository = userRepository;
        _hasher = hasher;
        _authUserCase = authUserCase;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request, CancellationToken ct)
    {
        var email = NormalizeEmail(request.Email);
        var auth = await _authUserCase.ExecuteAsync(request, ct);

    }



}