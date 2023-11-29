

using App.Application.Abstractions.Messaging;

namespace App.Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string Password):ICommand<Guid>;
