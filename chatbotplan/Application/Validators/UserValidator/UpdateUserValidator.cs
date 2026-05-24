using ChatBotPlan.Application.DTOS;
using FluentValidation;

namespace ChatBotPlan.Application;

public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Number)
            .MinimumLength(10).WithMessage("Número inválido")
            .When(x => !string.IsNullOrWhiteSpace(x.Number));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Name) ||
                       !string.IsNullOrWhiteSpace(x.Number))
            .WithMessage("Informe pelo menos um campo para atualizar");
    }
}