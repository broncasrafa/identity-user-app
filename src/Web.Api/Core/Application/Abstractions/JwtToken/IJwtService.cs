using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Application.Abstractions.JwtToken;

public interface IJwtService
{
    string GenerateToken(User user);
}
