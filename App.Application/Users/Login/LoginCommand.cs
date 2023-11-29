﻿
using App.Application.Abstractions.Messaging;

namespace App.Application.Users.Login;

public record LoginCommand(string Email, string Password) : ICommand<string>;