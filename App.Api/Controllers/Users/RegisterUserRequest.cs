namespace App.Api.Controllers.Users;

public sealed record RegisterUserRequest(
    string Email,
    string Password);