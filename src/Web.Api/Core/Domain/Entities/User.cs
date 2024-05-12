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

    public User()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public User(string username, string email, string firstName, string lastName, string cpf)
    {
        UserName = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Cpf = cpf;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }
}
