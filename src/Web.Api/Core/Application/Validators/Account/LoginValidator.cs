using Web.Api.Core.Application.DTO.Request.Account;
using Web.Api.Core.Application.Validators.ExtensionsValidators;
using FluentValidation;

namespace Web.Api.Core.Application.Validators.Account;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(c => c.UserName).UsernameValidations();

        RuleFor(c => c.Password).PasswordValidations();
    }
}
