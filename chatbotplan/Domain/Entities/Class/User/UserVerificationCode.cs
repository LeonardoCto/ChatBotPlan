namespace ChatBotPlan.Domain;

public class UserVerificationCode
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Code { get; set; }
    public DateTime Expiration { get; set; }
    public bool IsUsed { get; set; }
    public string? PendingValue { get; set; }
    public VerificationCodeType Type { get; set; }

    public bool IsValid(string code)
        => Code == code && Expiration > DateTime.UtcNow && !IsUsed;
    public void MarkAsUsed() => IsUsed = true;
}

public enum VerificationCodeType
{
    EmailChange,
    PasswordReset,
    TwoFactor
}