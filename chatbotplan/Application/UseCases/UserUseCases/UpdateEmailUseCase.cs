using System.Buffers.Text;
using System.Security.Cryptography;
using AutoMapper;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using ChatBotPlan.Domain.Interfaces;

namespace ChatBotPlan.Application;

public class UpdateEmailUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserVerificationCodeRepository _userVerificationCodeRepository;
    private readonly IEmailService _emailService;
    public UpdateEmailUseCase(IUserRepository userRepo, IUserValidator userValidator, IUnitOfWork unitOfWork, IUserVerificationCodeRepository userVerificationCodeRepository, IEmailService emailService)
    {
        _userRepository = userRepo;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
        _userVerificationCodeRepository = userVerificationCodeRepository;
        _emailService = emailService;
    }

    public async Task ExecuteAsync(string newEmail, UpdateUserDTO request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null)
            throw new DomainException($"User with email '{request.Email}' not found");

        await _userValidator.EnsureEmailIsUniqueAsync(newEmail);
        string code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        var verificationCode = UserVerificationCode.Create(code, user.Id, newEmail, VerificationCodeType.EmailChange);

        await _userVerificationCodeRepository.AddAsync(verificationCode, ct);
        await _unitOfWork.CommitAsync(ct);

        await _emailService.SendEmailAsync(request.Email, "Email change confirmation", $"Your confirmation code is: {code}");

    }

    public async Task ConfirmEmailChangeAsync(string code, UpdateUserDTO request, CancellationToken ct)
    {

        var user = await _userRepository.GetByEmailAsync(request.Email, ct);
        if (user == null)
            throw new DomainException("User not found");

        var verificationCode = await _userVerificationCodeRepository.GetActiveCodeAsync(code, user.Id, VerificationCodeType.EmailChange, ct);
        if (verificationCode == null)
            throw new DomainException("Invalid or expired verification code");

        user.UpdateEmail(verificationCode.PendingValue);
        verificationCode.IsUsed = true;
        _userRepository.Update(user);
        _userVerificationCodeRepository.Update(verificationCode);

        await _unitOfWork.CommitAsync(ct);
    }


}