using Microsoft.AspNetCore.Identity;
using Web.Api.Core.Application.Abstractions.Account;
using Web.Api.Core.Application.Abstractions.JwtToken;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;

    public AccountService(
        UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    #region [ private methods ]

    #endregion
}
