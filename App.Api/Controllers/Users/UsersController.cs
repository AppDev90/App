using App.Application.Users.Login;
using App.Application.Users.Register;
using App.Domain.Enums;
using App.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers.Users;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LogIn(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);

        var tokenResult = await _sender.Send(command, cancellationToken);

        if (tokenResult.IsFailure)
        {
            return Unauthorized(tokenResult.Error);
        }

        return Ok(tokenResult.Value);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
    RegisterUserRequest request,
    CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.Password);

        var user = await _sender.Send(command, cancellationToken);

        if (user.IsFailure)
        {
            return BadRequest(user.Error);
        }

        return Ok(user.Value);
    }


    [HasPermission(Permission.ReadUser)]
    [HttpGet("userpermission")]
    public async Task<IActionResult> UserPermission()
    {
        return Ok("ok");
    }


    [Authorize]
    [HttpGet("userauthorize")]
    public async Task<IActionResult> UserAuthorize()
    {
        return Ok("ok");
    }

}
