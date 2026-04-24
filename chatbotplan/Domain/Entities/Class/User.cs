namespace ChatBotPlan.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PassWordHash { get; private set; }
    public string Number { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public User() { }
    public static User Create(string name, string email, string number, string passWordHash)
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
            PassWordHash = passWordHash,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public void Update(string name, string email, string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(number);

        Name = name.Trim();
        Email = Email.Trim().ToUpperInvariant();
        UpdatedAt = DateTime.UtcNow;
    }
}