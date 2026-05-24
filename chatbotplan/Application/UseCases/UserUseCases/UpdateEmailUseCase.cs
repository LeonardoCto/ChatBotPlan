using System.Buffers.Text;
using System.Security.Cryptography;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatBotPlan.Application;

public class UpdateEmailUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;
    private readonly AzureEmailAdapter _emailService;

    public UpdateEmailUseCase(IUserRepository userRepo, IUserValidator userValidator, AzureEmailAdapter emailService)
    {
        _userRepository = userRepo;
        _userValidator = userValidator;
        _emailService = emailService;
    }

    public async Task<UpdateUserDTO> ExecuteAsync(string newEmail, UpdateUserDTO request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null)
            throw new Exception("User not found");

        await _userValidator.EnsureEmailIsUniqueAsync(newEmail);
        string code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();




        await _emailService.SendEmailAsync(request.Email, "Email change confirmation", $"Your confirmation code is: {code}");


    }


}