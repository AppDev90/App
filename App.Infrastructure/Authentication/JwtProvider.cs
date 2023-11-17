using App.Application.Abstractions.Authentication;
using App.Application.Abstractions.Clock;
using App.Domain.Users;
using App.Infrastructure.Clock;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly JwtOption _jwtOption;

    public JwtProvider(IDateTimeProvider dateTimeProvider, IOptions<JwtOption> jwtOption)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtOption = jwtOption.Value;
    }

    public string Generate(User user)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email.Value),
        };

        var signingCridentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOption.Issuer,
            _jwtOption.Audience,
            claims,
            null,
            _dateTimeProvider.UtcNow.AddDays(1),
            signingCridentials
            );

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
