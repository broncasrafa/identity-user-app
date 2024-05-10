using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Infrastructure.Database.Context;

public class IdentityUserDbContext : IdentityDbContext<User>
{
    public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("dbo");

        builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());

        builder.Entity<IdentityRole>()
                    .ToTable("TB_IDENTITY_ROLE");

        builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("TB_IDENTITY_ROLE_CLAIMS");

        builder.Entity<IdentityUserRole<string>>()
                .ToTable("TB_IDENTITY_USER_ROLES");

        builder.Entity<IdentityUserClaim<string>>()
                .ToTable("TB_IDENTITY_USER_CLAIMS");

        builder.Entity<IdentityUserLogin<string>>()
                .ToTable("TB_IDENTITY_USER_LOGINS");

        builder.Entity<IdentityUserToken<string>>()
                .ToTable("TB_IDENTITY_USER_TOKENS");
    }
}
