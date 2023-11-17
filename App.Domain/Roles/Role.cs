using App.Domain.Permissions;
using App.Domain.Users;
using App.Domain.Abstraction;
using App.Domain.Shared;
using App.Domain.UserRoles;

namespace App.Domain.Roles;

public sealed class Role : Entity<RoleId>
{

    private Role()
    {
    }

    private Role(
        RoleId roleId,
        Name name)
        : base(roleId)
    {
        Name = name;
    }


    public Name Name { get; private set; }


    public ICollection<Permission> Permissions { get; private set; }


    public static Result<Role> Create(
    Name name)
    {
        var role = new Role(
            RoleId.New(),
            name);

        return role;
    }

}
