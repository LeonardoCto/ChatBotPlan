using ChatBotPlan.Application.DTOS;
using FluentValidation;

namespace ChatBotPlan.Application;

public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("Name must have at least 3 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Number)
            .MinimumLength(10).WithMessage("Invalid number")
            .When(x => !string.IsNullOrWhiteSpace(x.Number));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) ||
                       !string.IsNullOrWhiteSpace(x.Number) ||
                       !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("At least one field (name, number, or email) must be provided for update");
    }
}