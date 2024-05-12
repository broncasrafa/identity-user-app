using FluentValidation;

namespace Web.Api.Core.Application.Validators.ExtensionsValidators;

public static class PasswordValidators
{
    public static IRuleBuilderOptions<T, string> PasswordValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 6)
    {
        return ruleBuilder
                .NotNull().WithMessage("Senha não deve ser nulo")
                .NotEmpty().WithMessage("Senha é obrigatório")
                .MinimumLength(minimumLength).WithMessage($"Senha deve conter pelo menos {minimumLength} caracteres")
                .Matches("[A-Z]").WithMessage("Senha deve conter pelo menos 1 letra maiúscula")
                .Matches("[a-z]").WithMessage("Senha deve conter pelo menos 1 letra minúscula")
                .Matches("[0-9]").WithMessage("Senha deve conter pelo menos 1 número")
                .Matches("[!*@#$%^&+=]").WithMessage("Senha deve conter pelo menos 1 caracter especial");
    }

    public static IRuleBuilderOptions<T, string> PasswordConfirmationValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 6)
    {
        return ruleBuilder
                .NotNull().WithMessage("Senha de confirmação não deve ser nulo")
                .NotEmpty().WithMessage("Senha de confirmação é obrigatório")
                .MinimumLength(minimumLength).WithMessage($"Senha de confirmação deve conter pelo menos {minimumLength} caracteres")
                .Matches("[A-Z]").WithMessage("Senha de confirmação deve conter pelo menos 1 letra maiúscula")
                .Matches("[a-z]").WithMessage("Senha de confirmação deve conter pelo menos 1 letra minúscula")
                .Matches("[0-9]").WithMessage("Senha de confirmação deve conter pelo menos 1 número")
                .Matches("[!*@#$%^&+=]").WithMessage("Senha de confirmação deve conter pelo menos 1 caracter especial");
    }
}
