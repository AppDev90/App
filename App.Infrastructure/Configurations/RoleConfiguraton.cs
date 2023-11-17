
using App.Domain.RolePermissions;
using App.Domain.Roles;
using App.Domain.Shared;
using App.Domain.UserRoles;
using App.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations;

public sealed class RoleConfiguraton : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.RoleTable);

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .HasConversion(roleId => roleId.Value, value => new RoleId(value));

        builder.Property(role => role.Name)
            .HasMaxLength(32)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

    }
}
