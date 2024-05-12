using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Application.DTO.Response.Account;

public record UserAuthenticatedResponse(
    string Uuid,
    string Username,
    string Email,
    string Firstname,
    string Lastname,
    string Cpf,
    bool EmailConfirmed,
    string Token);
