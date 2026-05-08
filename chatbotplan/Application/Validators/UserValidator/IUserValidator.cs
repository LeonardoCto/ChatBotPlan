namespace ChatBotPlan.Application.Interfaces;

public interface IUserValidator
{
    Task EnsureEmailIsUniqueAsync(string email);
}