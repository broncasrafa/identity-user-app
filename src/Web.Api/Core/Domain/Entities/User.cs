using System;
using Microsoft.AspNetCore.Identity;

namespace Web.Api.Core.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Cpf { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
