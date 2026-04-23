using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Infrastructure.Repositories;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> CommitAsync(CancellationToken ct = default)
    => context.SaveChangesAsync(ct);
}