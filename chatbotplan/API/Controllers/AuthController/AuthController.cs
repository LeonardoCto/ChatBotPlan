
using ChatBotPlan.Application;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Infrastructure;
using ChatBotPlan.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ChatBotPlan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly AuthUserUseCase _authUserCase;

    public AuthController(AuthUserUseCase authUserCase)
    {
        _authUserCase = authUserCase;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request, CancellationToken ct)
    {
        var auth = await _authUserCase.ExecuteAsync(request, ct);
        return Ok(auth);
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDto, CancellationToken ct)
    {
        var refresh = await _authUserCase.RefreshTokenAsync(tokenDto, ct);
        return Ok(refresh);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [Route("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeTokenDTO request, CancellationToken ct)
    {
        await _authUserCase.RevokeTokenAsync(request, ct);
        return NoContent();
    }
}