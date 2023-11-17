using App.Domain.Abstraction;
using App.Domain.Shared;

namespace App.Domain.Permissions;

public sealed class Permission : Entity<PermissionId>
{
    public Name Name { get; set; }
}
