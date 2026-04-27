using AutoMapper;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class GetByIdUserCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetByIdUserCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDTO> GetById(Guid id, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
            throw new UserNotFoundException(id);

        return _mapper.Map<UserResponseDTO>(user);

    }
}