using System.Threading;
using System.Threading.Tasks;
using Web.Api.Core.Application.Abstractions.Account;
using Web.Api.Core.Application.DTO.Request.Account;
using Web.Api.Core.Application.Validators.ExtensionsValidators;
using FluentValidation;

namespace Web.Api.Core.Application.Validators.Account;

public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
{
    private readonly IAccountService _accountService;

    public UserRegistrationValidator(IAccountService accountService)
    {
        _accountService = accountService;


        RuleFor(c => c.FirstName)
            .NotNull().WithMessage("FirstName não deve ser nulo")
            .NotEmpty().WithMessage("FirstName é obrigatório")
            .Length(3, 15).WithMessage($"FirstName deve ter entre 3 e 15 caracteres");

        RuleFor(c => c.LastName)
            .NotNull().WithMessage("LastName não deve ser nulo")
            .NotEmpty().WithMessage("LastName é obrigatório")
            .Length(3, 45).WithMessage($"LastName deve ter entre 3 e 45 caracteres");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("E-mail não deve ser nulo")
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .MaximumLength(200).WithMessage("E-mail não deve exceder a 200 caracteres")
            .MustAsync(VerificarSeJaExisteEmailAsync).WithMessage("E-mail já está cadastrado.")
            .EmailAddress();

        RuleFor(c => c.UserName)
                .UsernameValidations()
                .MustAsync(VerificarSeJaExisteUserNameAsync).WithMessage("Username já está cadastrado.");

        RuleFor(c => c.Cpf).CpfValidations();

        RuleFor(c => c.Password).PasswordValidations();

        RuleFor(c => c.ConfirmPassword)
            .PasswordConfirmationValidations()
            .Equal(c => c.Password).WithMessage("Senha de confirmação não confere com a senha escolhida");
        
    }

    private async Task<bool> VerificarSeJaExisteEmailAsync(string email, CancellationToken cancellationToken)
    {
        return !await _accountService.CheckIfEmailAlreadyExistsAsync(email);
    }
    private async Task<bool> VerificarSeJaExisteUserNameAsync(string username, CancellationToken cancellationToken)
    {
        return !await _accountService.CheckIfUserNameAlreadyExistsAsync(username); ;
    }
}
