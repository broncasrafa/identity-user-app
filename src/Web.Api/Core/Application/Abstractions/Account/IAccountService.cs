using System.Threading.Tasks;
using Web.Api.Core.Application.DTO.Request;

namespace Web.Api.Core.Application.Abstractions.Account;

public interface IAccountService
{
    Task CreateUserAsync(UserRegistrationRequest user);
}
