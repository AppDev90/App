
using App.Domain.Enums;
using Microsoft.AspNetCore.Authorization;


namespace App.Infrastructure.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission)
        : base(policy: permission.ToString())
    {

    }
}
