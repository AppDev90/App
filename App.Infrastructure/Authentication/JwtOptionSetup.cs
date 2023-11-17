﻿using App.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace App.Api.OptionSetup;

public class JwtOptionSetup : IConfigureOptions<JwtOption>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOption options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
