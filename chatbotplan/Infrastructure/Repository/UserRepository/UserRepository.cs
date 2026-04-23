
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatBotPlan.Infrastructure.Repositories;

public class UserRepository (AppDbContext context): IUserRepository
{
    public  Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    => context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    => context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
    => await context.Users.ToListAsync(ct);
    public async Task AddAsync(User user, CancellationToken ct)
    => await context.Users.AddAsync(user, ct);

    public void Update(User user)
    => context.Users.Update(user);
    
    public void Delete(User user)
        => context.Users.Remove(user);

}
