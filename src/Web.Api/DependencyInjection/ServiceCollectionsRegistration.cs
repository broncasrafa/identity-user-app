using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Infrastructure.Database.Context;
using Web.Api.Core.Application.Settings;
using Web.Api.Core.Application.Validators.User;
using Web.Api.Core.Application.Abstractions.JwtToken;
using Web.Api.Core.Application.Services;
using Web.Api.Core.Domain.Entities;
using FluentValidation;
using Web.Api.Core.Application.Abstractions.Account;

namespace Web.Api.DependencyInjection;

public static class ServiceCollectionsRegistration
{
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();
    }

    public static void AddIdentityUserDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityUserDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityUserConnection"),
                c => c.MigrationsAssembly(typeof(IdentityUserDbContext).Assembly.FullName))
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();

        });
    }

    public static void AddIdentityConfigurations(this IServiceCollection services)
    {
        var passwordOptions = new PasswordOptions
        {
            RequiredLength = 6,
            //RequireDigit = false,
            //RequireLowercase = false,
            //RequireUppercase = false,
            //RequireNonAlphanumeric = false,
        };
        var signInOptions = new SignInOptions
        {
            RequireConfirmedEmail = true
        };

        services.AddIdentityCore<User>(options =>
        {
            options.Password = passwordOptions;
            options.SignIn = signInOptions;
        })
            .AddRoles<IdentityRole>() // ser capaz de adicionar roles
            .AddRoleManager<RoleManager<IdentityRole>>() // ser capaz de fazer uso do RoleManager (gerenciar as roles)
            .AddEntityFrameworkStores<IdentityUserDbContext>() // fornecendo o contexto da aplicação
            .AddSignInManager<SignInManager<User>>() // fazer uso do Sigin manager
            .AddUserManager<UserManager<User>>() // fazer uso do UserManager para criar usuários
            .AddDefaultTokenProviders(); // ser capaz de criar tokens para a confirmação de mail
    }

    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAccountService, AccountService>();
    }

    public static void AddInfrastructure(this IServiceCollection services)
    {
        
    }

    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
    }
}
