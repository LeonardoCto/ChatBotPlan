using ChatBotPlan.Domain.Entities;

public class ApplicationUser : User
{
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiretTime { get; set; }
}