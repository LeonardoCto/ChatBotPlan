
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserUseCase(IUserRepository userRepo, IUnitOfWork unit)
    {
        _userRepository = userRepo;
        _unitOfWork = unit;
    }
    public async Task<UserResponseDTO> ExecuteAsync(Guid id, UpdateUserDTO request, CancellationToken ct)
    {
        User user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
            throw new DomainException($"User with id '{id}' not found");

        user.Update(request.Name, request.Number);

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(ct);

        return user.MapToResponse();

    }
}