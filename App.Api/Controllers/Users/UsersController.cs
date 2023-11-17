using App.Application.Users.Login;
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


    [HasPermission(Permission.ReadMember)]
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
