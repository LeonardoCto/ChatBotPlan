using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Infrastructure.Repositories;

namespace ChatBotPlan.Application;

public class DeleteUserCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteUser(Guid id, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
            throw new UserNotFoundException(id);

        _userRepository.Delete(user);

        await _unitOfWork.CommitAsync();
    }
}