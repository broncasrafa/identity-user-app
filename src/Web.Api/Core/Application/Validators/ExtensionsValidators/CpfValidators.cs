using FluentValidation;
using Web.Api.Core.Application.Extensions;
using Web.Api.Core.Application.Utils;

namespace Web.Api.Core.Application.Validators.ExtensionsValidators;

public static class CpfValidators
{
    public static IRuleBuilderOptions<T, string> CpfValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotNull().WithMessage("Cpf não deve ser nulo")
                .NotEmpty().WithMessage("Cpf é obrigatório")
                .Must(BeAValidCpfLength).WithMessage("Cpf deve conter 11 caracteres")
                .Must(Util.IsValidCpf).WithMessage("Cpf inválido");
    }


    private static bool BeAValidCpfLength(string cpf)
    {
        cpf = cpf.RemoverFormatacaoCpfCnpj();
        if (cpf != null && cpf.Length == 11) return true;
        return false;
    }
}
