using Azure.Core;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class CreateUsersUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _hasher;
    private readonly IUserValidator _userValidator;

    public CreateUsersUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserValidator userValidator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hasher = passwordHasher;
        _userValidator = userValidator;
    }

    public async Task<UserResponseDTO> ExecuteAsync(UserRequestDTO request, CancellationToken ct)
    {
        string email = NormalizeEmail(request.Email);

        await _userValidator.EnsureEmailIsUniqueAsync(email);

        User user = CreateUser(request, email);

        await _userRepository.AddAsync(user, ct);
        await _unitOfWork.CommitAsync(ct);

        return user.MapToResponse();
    }

    private static string NormalizeEmail(string email)
    => email.Trim().ToLowerInvariant();

    private User CreateUser(UserRequestDTO request, string email)
    {
        string hash = _hasher.Hash(request.PassWord);
        return User.Create(request.Name, email, request.Number, hash);
    }
}