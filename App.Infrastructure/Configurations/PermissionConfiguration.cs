using App.Domain.Permissions;
using App.Domain.Shared;
using App.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.PermissionTable);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(p => p.Value, Value => new PermissionId(Value));

        builder.Property(p => p.Name)
            .HasConversion(p => p.Value, Value => new Name(Value));
    }
}
