using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task EnsureEmailIsUniqueAsync(string email)
    {
        var existing = await _userRepository.GetByEmailAsync(email);
        if (existing != null)
            throw new EmailAlreadyInUseException(email);
    }
}