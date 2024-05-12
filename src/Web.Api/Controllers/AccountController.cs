using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Api.Core.Application.Abstractions.Account;
using Web.Api.Core.Application.DTO.Request.Account;
using Web.Api.Core.Application.DTO.Response.Account;

namespace Web.Api.Controllers;

[Route("api/v1/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }


    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var authenticatedUser = await _accountService.AuthenticateUserAsync(request.UserName, request.Password);
        if (authenticatedUser is null)
            return Unauthorized("Invalid username or password");

        if (!authenticatedUser.EmailConfirmed)
            return Unauthorized("User e-mail not confirmed");

        return Ok(authenticatedUser);
    }


    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
    {
        UserResponse newUser = await _accountService.RegisterUserAsync(request);
        if (newUser is null)
            return BadRequest(new { message = "Não foi possível registrar os dados" });

        return Ok(newUser);
    }


    [Authorize]
    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshUserToken()
    {
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Usuário não autenticado" });

        var token = await _accountService.RefreshUserTokenAsync(userId);
        return Ok(token);
    }
}
