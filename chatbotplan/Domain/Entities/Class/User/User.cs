namespace ChatBotPlan.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PassWord { get; private set; }
    public string Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    public UserRoles Role { get; private set; }

    public User() { }
    public static User Create(string name, string email, string number, string passWordHash, UserRoles role = UserRoles.User)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(number);
        ArgumentException.ThrowIfNullOrWhiteSpace(passWordHash);

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Trim().ToLowerInvariant(),
            Number = number.Trim(),
            PassWord = passWordHash,
            CreatedAt = DateTime.UtcNow,
            Role = role
        };
    }

    public void Update(string name, string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(number);

        Name = name.Trim();
        Number = number.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
            Email = email.Trim().ToLowerInvariant();

        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdatePassWord(string passWordHash)
    {
        if (!string.IsNullOrEmpty(passWordHash))
            PassWord = passWordHash;

        UpdatedAt = DateTime.UtcNow;
    }

    public void SetRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiryTime = expiry;
    }
}