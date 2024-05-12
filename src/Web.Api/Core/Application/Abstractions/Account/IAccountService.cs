using System.Threading.Tasks;
using Web.Api.Core.Application.DTO.Request.Account;
using Web.Api.Core.Application.DTO.Response.Account;

namespace Web.Api.Core.Application.Abstractions.Account;

public interface IAccountService
{
    Task<UserAuthenticatedResponse> AuthenticateUserAsync(string username, string password);
    Task<UserResponse> RegisterUserAsync(UserRegistrationRequest user);
    Task<string> RefreshUserTokenAsync(string userId);

    Task<bool> CheckIfEmailAlreadyExistsAsync(string email);
    Task<bool> CheckIfUserNameAlreadyExistsAsync(string username);
}
