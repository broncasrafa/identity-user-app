using System;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Application.DTO.Response.Account;

public record UserResponse(
    string Id,
    string UserName, 
    string Email, 
    string FirstName,      
    string LastName, 
    string Cpf,
    bool IsActive,
    DateTime CreatedAt)
{
    public static UserResponse Map(User user)
    {
        return new UserResponse(user.Id, user.UserName, user.Email, user.FirstName, user.LastName, user.Cpf, user.IsActive, user.CreatedAt);
    }
}
