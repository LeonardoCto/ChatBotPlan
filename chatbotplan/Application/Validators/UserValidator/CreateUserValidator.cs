using ChatBotPlan.Application.DTOS;
using FluentValidation;

namespace ChatBotPlan.Application;

public class CreateUserValidator : AbstractValidator<UserRequestDTO>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Número é obrigatório")
            .MinimumLength(10).WithMessage("Número inválido");

        RuleFor(x => x.PassWord)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres");
    }
}