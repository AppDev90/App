using App.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace App.Infrastructure.Authentication;
public class PermissionAuthorizationHandler :
    AuthorizationHandler<PermissionRequirement>
{

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(
            p => p.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (!Guid.TryParse(userId, out Guid parsedUserId))
        {
            return;
        }

        var userID = new UserId(parsedUserId);

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        HashSet<string> permissions = await permissionService
            .GetPermissionAsync(userID);

        if(permissions.Contains(requirement.Permission)) 
        {
            context.Succeed(requirement);
        }
    }
}
