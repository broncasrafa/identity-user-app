using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("TB_IDENTITY_USER");

        builder.Property(c => c.FirstName)
            .HasColumnName("FirstName")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .HasColumnName("LastName")
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(c => c.Cpf)
            .HasColumnName("CPF")
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(c => c.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime");

        builder.Property(c => c.IsActive)
            .HasColumnName("IsActive")
            .IsRequired()
            .HasColumnType("bit")
            .HasDefaultValueSql("(1)");
    }
}
