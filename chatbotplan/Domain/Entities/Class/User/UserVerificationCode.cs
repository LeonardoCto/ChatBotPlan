namespace ChatBotPlan.Domain;

public class UserVerificationCode
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Code { get; private set; }
    public DateTime Expiration { get; private set; }
    public bool IsUsed { get; set; }
    public string? PendingValue { get; set; }
    public VerificationCodeType Type { get; set; }
    private UserVerificationCode() { }

    public void MarkAsUsed()
    => IsUsed = true;
    public static UserVerificationCode Create(string code, Guid userId, string pendingValue, VerificationCodeType type)
    {
        return new UserVerificationCode
        {
            Id = Guid.NewGuid(),
            Code = code,
            UserId = userId,
            PendingValue = pendingValue,
            Type = type,
            IsUsed = false,
            Expiration = DateTime.UtcNow.AddMinutes(15)
        };
    }
}

public enum VerificationCodeType
{
    EmailChange,
    PasswordReset,
    TwoFactor
}