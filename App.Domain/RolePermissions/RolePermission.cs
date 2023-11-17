using App.Domain.Abstraction;
using App.Domain.Permissions;
using App.Domain.Roles;
using App.Domain.Users;

namespace App.Domain.RolePermissions;

public sealed class RolePermission
{

    private RolePermission()
    {

    }

    private RolePermission(
        RoleId roleId,
        PermissionId permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public RoleId RoleId { get; set; }

    public PermissionId PermissionId { get; set; }


    public static Result<RolePermission> Create(
       RoleId roleId,
       PermissionId permissionId)
    {
        var rolePermission = new RolePermission(
            roleId,
            permissionId);

        return rolePermission;
    }

}
