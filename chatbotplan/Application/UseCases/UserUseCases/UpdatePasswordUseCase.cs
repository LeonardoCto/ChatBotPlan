using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Domain;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;
using ChatBotPlan.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotPlan.Application;

public class UpdatePasswordUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher, IEmailService emailService, IUserVerificationCodeRepository verificationCodeRepository, IUnitOfWork unitOfWork)
{

    public async Task ExecuteAsync(UpdatePassWordDTO request, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null)
            throw new DomainException("User not found");

        var verifyPassword = passwordHasher.Verify(request.PassWord, user.PassWord);
        if (!verifyPassword)
            throw new DomainException("Incorrect password");

        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        await emailService.SendEmailAsync(user.Email, "Password Change request", $"Here is your code {code} ");

        var newPassWordHash = passwordHasher.Hash(request.NewPassWord);

        var verificationCode = UserVerificationCode.Create(code, user.Id, newPassWordHash, VerificationCodeType.PasswordReset);

        await verificationCodeRepository.AddAsync(verificationCode, ct);
        await unitOfWork.CommitAsync(ct);

    }

    public async Task ConfirmPassWordChange(UpdatePassWordDTO request, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null)
            throw new DomainException("User not found");

        var verificationCode = await verificationCodeRepository.GetActiveCodeAsync(request.Code, user.Id, VerificationCodeType.PasswordReset, ct);
        if (verificationCode == null)
            throw new DomainException("Invalid or expired verification code");

        user.UpdatePassWord(verificationCode.PendingValue);
        verificationCode.MarkAsUsed();

        userRepository.Update(user);
        verificationCodeRepository.Update(verificationCode);
        await unitOfWork.CommitAsync(ct);

    }
}