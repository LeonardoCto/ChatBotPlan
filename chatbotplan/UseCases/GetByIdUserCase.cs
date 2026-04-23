using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class GetByIdUserCase
{
    private readonly IUserRepository _userRepository;

    public GetByIdUserCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // public UserResponseDTO GetById(Guid id, CancellationToken ct)
    // {
    //     var user = _userRepository.GetByIdAsync(id, ct);
    //     if(user == null)
    //     throw new UserNotFoundException(id);

    //     return User.MapToResponse(user);
      
    // }
}