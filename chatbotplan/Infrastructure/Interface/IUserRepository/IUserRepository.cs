using System.Security.Cryptography;
using System.Xml.Serialization;
using ChatBotPlan.Domain.Entities;

namespace ChatBotPlan.Domain.Interfaces;
public interface IUserRepository
{
    Task AddAsync (User user, CancellationToken ct = default);
    Task <User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task <User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
    void Update( User user);
    void Delete(User user);

}