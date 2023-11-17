
using App.Domain.Abstraction;
using App.Domain.Roles;
using App.Domain.Users;

namespace App.Domain.UserRoles;

public sealed class UserRole
{

    private UserRole()
    {

    }

    private UserRole(
        UserId userId,
        RoleId roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public UserId UserId { get; set; }
    
    public RoleId RoleId { get; set; }


    public static Result<UserRole> Create(
        UserId userId,
        RoleId roleId)
    {
        var userRole = new UserRole(
            userId,
            roleId);

        return userRole;
    }
}
