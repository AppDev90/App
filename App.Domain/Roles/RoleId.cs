

namespace App.Domain.Roles;

public sealed record RoleId(Guid Value)
{
    public static RoleId New() => new(Guid.NewGuid());
}
