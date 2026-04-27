using AutoMapper;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDTO>();
    }
}