using ChatBotPlan.Domain;
using ChatBotPlan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatBotPlan.Infrastructure.Repositories;

public class UserVerificationCodeRepository(AppDbContext context) : IUserVerificationCodeRepository
{
    public async Task AddAsync(UserVerificationCode code, CancellationToken ct)
    => await context.UserVerificationCodes.AddAsync(code, ct);

    public void Delete(UserVerificationCode code)
    => context.UserVerificationCodes.Remove(code);

    public async Task<UserVerificationCode?> GetActiveCodeAsync(string code, Guid userId, VerificationCodeType type, CancellationToken ct)
    => await context.UserVerificationCodes.FirstOrDefaultAsync(c => c.UserId == userId
    && c.Type == type
    && c.Code == code
    && c.Expiration > DateTime.UtcNow
    && !c.IsUsed, ct);

    public void Update(UserVerificationCode code)
    => context.UserVerificationCodes.Update(code);
}