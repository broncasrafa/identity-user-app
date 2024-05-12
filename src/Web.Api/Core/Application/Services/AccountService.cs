using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Api.Core.Application.Abstractions.Account;
using Web.Api.Core.Application.Abstractions.JwtToken;
using Web.Api.Core.Application.DTO.Request.Account;
using Web.Api.Core.Application.DTO.Response.Account;
using Web.Api.Core.Application.Exceptions;
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



    public async Task<bool> CheckIfEmailAlreadyExistsAsync(string email) 
        => await _userManager.Users.AnyAsync(c => c.Email == email.ToLower());

    public async Task<bool> CheckIfUserNameAlreadyExistsAsync(string username)
        => await _userManager.Users.AnyAsync(c => c.UserName == username.ToLower());




    public async Task<UserAuthenticatedResponse> AuthenticateUserAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Value cannot be null or empty. (Parameter 'username')", nameof(username));
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or empty. (Parameter 'password')", nameof(password));

        var user = await _userManager.FindByNameAsync(username);

        if (user is null) return null;
        if (!user.EmailConfirmed)
            return new UserAuthenticatedResponse(user.Id, user.UserName, user.Email, user.FirstName, user.LastName, user.Cpf, user.EmailConfirmed, Token: null);

        var result = await _signInManager.CheckPasswordSignInAsync(user: user, password: password, lockoutOnFailure: false);
        //.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);
        if (result is null || !result.Succeeded)
            return null;

        var token = _jwtService.GenerateToken(user);
        return new UserAuthenticatedResponse(user.Id, user.UserName, user.Email, user.FirstName, user.LastName, user.Cpf, user.EmailConfirmed, token);
    }

    public async Task<UserResponse> RegisterUserAsync(UserRegistrationRequest user)
    {
        if (await CheckIfEmailAlreadyExistsAsync(user.Email))
            throw new UserAlreadyExistsException("Usuário já existente com o e-mail informado");

        if (await CheckIfUserNameAlreadyExistsAsync(user.UserName))
            throw new UserAlreadyExistsException("Usuário já existente com o username informado");

        var newUser = new User(username: user.UserName, email: user.Email, firstName: user.FirstName, lastName: user.LastName, cpf: user.Cpf);
        var result = await _userManager.CreateAsync(user: newUser, password: user.Password);
        if (result.Succeeded)
        {
            // registra o perfil do usuario
            //await _userManager.AddToRoleAsync(newUser, RoleType.User.ToString());
            return UserResponse.Map(newUser);
        }

        return null;
    }

    public async Task<string> RefreshUserTokenAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Value cannot be null or empty. (Parameter 'userId')", nameof(userId));

        var user = await _userManager.FindByIdAsync(userId);

        var token = _jwtService.GenerateToken(user);
        //return new UserAuthenticatedResponse(user.UserName, user.Email, user.FirstName, user.LastName, user.Cpf, user.EmailConfirmed, token);

        return token;
    }
}
