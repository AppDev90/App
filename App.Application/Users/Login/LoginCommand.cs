
using App.Application.Abstractions.Messaging;
using App.Domain.Users;

namespace App.Application.Users.Login;

public record LoginCommand(string Email, string Password) : ICommand<string>;