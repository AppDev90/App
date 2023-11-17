using App.Domain.Users;

namespace App.Api.Controllers.Users;

public record LoginRequest(string Email, string Password);
