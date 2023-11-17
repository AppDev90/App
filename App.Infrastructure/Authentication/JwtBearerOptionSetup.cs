using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.Infrastructure.Authentication;

public class JwtBearerOptionSetup : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOption _jwtOption;

    public JwtBearerOptionSetup(IOptions<JwtOption> jwtOption)
    {
        _jwtOption = jwtOption.Value;
    }

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOption.Issuer,
            ValidAudience = _jwtOption.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_jwtOption.SecretKey))

        };
    }
}
