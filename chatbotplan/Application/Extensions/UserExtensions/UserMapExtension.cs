using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain.Entities;

namespace ChatBotPlan.Application;

public static class UserMapExtension
{
    public static UserResponseDTO MapToResponse(this User user)
    => new(user.Id, user.Email, user.Name, user.Number);

}