using Azure.Core;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;
public class CreateUsersCases
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _hasher;

    public CreateUsersCases(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hasher = passwordHasher;
    }

    public async Task<UserResponseDTO> ExecuteAsync(CreateUserRequestDTO request, CancellationToken ct)
    {
        string email = NormalizeEmail(request.Email);

        await EnsureEmailIsUnique(email, ct);

        User user = CreateUser(request, email);

        await _userRepository.AddAsync(user, ct);
        await _unitOfWork.CommitAsync(ct);

        return MapToResponse(user);
    }

    private static string NormalizeEmail(string email)
    => email.Trim().ToLowerInvariant();

    private async Task EnsureEmailIsUnique(string email, CancellationToken ct)
    {
        var existing = await _userRepository.GetByEmailAsync(email, ct);
        if(existing != null)
        throw new EmailAlreadyInUseException(email);

    }

    private User CreateUser(CreateUserRequestDTO request, string email)
    {
        string hash = _hasher.Hash(request.PassWordHash);
        return User.Create(request.Name, email, request.Number, hash);
    }

    private static UserResponseDTO MapToResponse(User user)
    => new(user.Id, user.Email, user.Name, user.Number);

}