
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class UpdateUserCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserCase(IUserRepository userRepo, IUnitOfWork unit)
    {
        _userRepository = userRepo;
        _unitOfWork = unit;
    }
    public async Task<UserResponseDTO> ExecuteAsync(Guid id, UpdateUserDTO request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Name) &&
            string.IsNullOrWhiteSpace(request.Email) &&
            string.IsNullOrWhiteSpace(request.Number))
            throw new NoFieldsException();

        User user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
            throw new UserNotFoundException(id);

        user.UpdatePartial(request.Name, request.Number);

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync(ct);

        return user.MapToResponse();

    }
}