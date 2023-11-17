

using App.Domain.Abstraction;
using App.Domain.Roles;
using App.Domain.Shared;
using App.Domain.UserRoles;

namespace App.Domain.Users;

public class User : Entity<UserId>
{

    private User()
    {

    }

    private User(
        UserId userId,
        Email email,
        string passwordHash)
      : base(userId)
    {
        Email = email;
        PasswordHash = passwordHash;
    }


    public Email Email { get; private set; }

    public string PasswordHash { get; private set; }


    public ICollection<Role> Roles { get; private set; }


    public static Result<User> Create(
        Email email,
        string passwordHash)
    {
        var user = new User(
            UserId.New(),
            email,
            passwordHash);

        return user;
    }

}
