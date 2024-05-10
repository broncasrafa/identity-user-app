using Web.Api.Core.Application.DTO.Request;
using FluentValidation;

namespace Web.Api.Core.Application.Validators.User;

public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}
