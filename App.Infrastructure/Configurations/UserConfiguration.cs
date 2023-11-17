
using App.Domain.RolePermissions;
using App.Domain.Roles;
using App.Domain.Shared;
using App.Domain.UserRoles;
using App.Domain.Users;
using App.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.UserTable);

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id)
                .HasConversion(userId => userId.Value, value => new UserId(value));

            builder.Property(user => user.Email)
                .HasMaxLength(64)
                .HasConversion(name => name.Value, value => Email.Create(value).Value);

            builder.HasMany(user => user.Roles)
                .WithMany()
                .UsingEntity<UserRole>();

        }
    }
}
