using App.Domain.Permissions;
using App.Domain.RolePermissions;
using App.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = App.Domain.Enums.Permission;

namespace App.Infrastructure.Configurations;

public class RolePermissionConfigurations : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(p => new { p.RoleId, p.PermissionId });

        builder.Property(p => p.RoleId)
            .HasConversion(roleId => roleId.Value, value => new RoleId(value));

        builder.Property(p => p.PermissionId)
            .HasConversion(permissionId => permissionId.Value, value => new PermissionId(value));

    }
}
