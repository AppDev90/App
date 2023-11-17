
using App.Domain.Users;

namespace App.Infrastructure.Authentication;
public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionAsync(UserId userId);
}
