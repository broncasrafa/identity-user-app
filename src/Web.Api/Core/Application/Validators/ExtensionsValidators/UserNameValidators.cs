using FluentValidation;

namespace Web.Api.Core.Application.Validators.ExtensionsValidators;

public static class UserNameValidators
{
    public static IRuleBuilderOptions<T, string> UsernameValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 3, int maximumLength = 15)
    {
        return ruleBuilder
                .NotNull().WithMessage("Username não deve ser nulo")
                .NotEmpty().WithMessage("Username é obrigatório")
                .Length(minimumLength, maximumLength).WithMessage($"Username deve ter entre {minimumLength} e {maximumLength} caracteres");
    }
}
