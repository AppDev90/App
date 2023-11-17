
using App.Domain.Users;
using App.Infrastructure;
using App.Domain.Permissions;
using App.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using App.Domain.Roles;
using App.Application.Utility;
using App.Domain.UserRoles;
using App.Domain.RolePermissions;

namespace App.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var user = dbContext.Set<User>();
        var role = dbContext.Set<Role>();
        var permission = dbContext.Set<Permission>();
        var userRole = dbContext.Set<UserRole>();
        var rolePermission = dbContext.Set<RolePermission>();

        var localPermissions = GetLocalPermissions();

        if (DbIsEmpty(permission))
        {
            var initUser = GetInitUser();
            var initRole = GetInitRole();
            var initUserRole = GetInitUserRole(initUser, initRole);
            var initRolePermissions = GetInitRolePermission(localPermissions, initRole);

            user.Add(initUser);
            role.Add(initRole);
            permission.AddRange(localPermissions);
            userRole.Add(initUserRole);
            rolePermission.AddRange(initRolePermissions);

            dbContext.SaveChanges();
        }
        else
        {
            var dbPermissions = permission.ToList();

            if (dbPermissions.Count() != localPermissions.Count())
            {
                //IEnumerable<Permission> newPermissions =
                //  localPermissions.Except(dbPermissions);

                var dbIds = dbPermissions.Select(p => p.Id).ToList();
                List<Permission> newPermissions = new();

                foreach (var localPermission in localPermissions)
                {
                    if (!dbIds.Contains(localPermission.Id))
                    {
                        newPermissions.Add(localPermission);
                    }
                }

                permission.AddRange(newPermissions);

                dbContext.SaveChanges();
            }
        }
    }

    private static List<RolePermission> GetInitRolePermission(
        IEnumerable<Permission> localPermissions,
        Role initRole)
    {
        List<RolePermission> rolePermissions = new();

        foreach (var perm in localPermissions)
        {
            rolePermissions.Add(
                RolePermission.Create(
                    initRole.Id,
                    perm.Id
                    ).Value);
        }

        return rolePermissions;
    }

    private static UserRole GetInitUserRole(User initUser, Role initRole)
    {
        return UserRole.Create(
            initUser.Id,
            initRole.Id).Value;
    }

    private static Role GetInitRole()
    {
        return Role.Create(
                        new Name("Admin")
                        ).Value;
    }

    private static User GetInitUser()
    {
        return User.Create(
            Email.Create("sya@gmail.com").Value,
            SHA1.Encode("12345@")).Value;
    }

    private static IEnumerable<Permission> GetLocalPermissions()
    {
        return Enum.GetValues<Domain.Enums.Permission>()
                    .Select(p => new Permission()
                    {
                        Id = new PermissionId((int)p),
                        Name = new Name(p.ToString())
                    });
    }

    private static bool DbIsEmpty(DbSet<Permission> permission)
    {
        return permission.Count() == 0;
    }

}
