namespace ChatBotPlan.Infrastructure;

public class TokenSettings
{
    public string SecretKey { get; set; } = default!;
    public string ValidIssuer { get; set; } = default!;
    public string ValidAudience { get; set; } = default!;
    public double TokenValidityMinutes { get; set; }
}