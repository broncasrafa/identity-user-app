using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Application.Validators.User;
using Web.Api.Infrastructure.Database.Context;

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
}
