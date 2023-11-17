
using App.Domain.Users;

namespace App.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}
