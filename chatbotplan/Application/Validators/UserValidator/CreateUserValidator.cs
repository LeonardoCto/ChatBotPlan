using ChatBotPlan.Application.DTOS;
using FluentValidation;

namespace ChatBotPlan.Application;

public class CreateUserValidator : AbstractValidator<UserRequestDTO>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Field name is required")
            .MinimumLength(3).WithMessage("Name must have at least 3 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Number is required")
            .MinimumLength(10).WithMessage("Invalid number");

        RuleFor(x => x.PassWord)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must have at least 6 characters");
    }
}