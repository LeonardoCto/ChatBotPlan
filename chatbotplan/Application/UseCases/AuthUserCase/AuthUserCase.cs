using System.Security.Authentication;
using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class AuthUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _hasher;
    private readonly IUserValidator _userValidator;

    public AuthUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserValidator userValidator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hasher = passwordHasher;
        _userValidator = userValidator;
    }

    public async Task<LoginResponseDTO> ExecuteAsync(LoginRequestDTO request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null)
            throw new InvalidCredentialException();



    }
}