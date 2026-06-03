using System.Security.Claims;
using ChatBotPlan.Application;

namespace ChatBotPlan.Infrastructure;

public class UserContext(IHttpContextAccessor _acessor) : IUserContext
{

    public string? userId => _acessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}