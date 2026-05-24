
namespace ChatBotPlan.Domain.Interfaces;

public interface IUserVerificationCodeRepository
{
    Task AddAsync(UserVerificationCode code, CancellationToken ct);
    Task<UserVerificationCode?> GetActiveCodeAsync(string code, Guid userId, VerificationCodeType type, CancellationToken ct);
    void Delete(UserVerificationCode code);
    void Update(UserVerificationCode code);
}