
using App.Domain.Roles;
using App.Domain.UserRoles;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(p => new { p.UserId, p.RoleId });

            builder.Property(p => p.RoleId)
                .HasConversion(roleId => roleId.Value, value => new RoleId(value));

            builder.Property(p => p.UserId)
                .HasConversion(userId => userId.Value, value => new UserId(value));
        }
    }
}
