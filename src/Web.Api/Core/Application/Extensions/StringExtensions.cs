using System;
using Web.Api.Core.Application.Utils;

namespace Web.Api.Core.Application.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Informa se a string do cpf é valida
    /// </summary>
    /// <param name="cpf">string do cpf</param>
    /// <returns>true se for valido.</returns>
    public static bool IsValidCpf(this string cpf)
    {
        return Util.IsValidCpf(cpf);
    }

    /// <summary>
    /// Remove a formatação do cnpj
    /// </summary>
    /// <param name="value">valor da string de cnpj</param>
    /// <returns>string de cnpj sem formatação</returns>
    public static string RemoverFormatacaoCpfCnpj(this string value)
    {
        if (String.IsNullOrEmpty(value)) return null;

        var result = value.Replace(".", "")
                    .Replace(".", "")
                    .Replace("/", "")
                    .Replace("-", "")
                    .Trim();

        return string.Join("", result.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
    }
}
