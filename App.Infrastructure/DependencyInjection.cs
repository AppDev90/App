using App.Api.OptionSetup;
using App.Application.Abstractions.Authentication;
using App.Application.Abstractions.Clock;
using App.Application.Abstractions.Data;
using App.Domain.Abstraction;
using App.Domain.Users;
using App.Infrastructure.Authentication;
using App.Infrastructure.Clock;
using App.Infrastructure.Data;
using App.Infrastructure.Outbox;
using App.Infrastructure.Repositories;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.IdentityModel.Tokens.Jwt;

namespace App.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        //services.AddTransient<IEmailService, EmailService>();

        AddPersistence(services, configuration);

        AddAuthentication(services, configuration);

        AddBackgroundJob(services, configuration);

        return services;
    }

    private static void AddBackgroundJob(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {

        services.ConfigureOptions<JwtOptionSetup>();
        services.ConfigureOptions<JwtBearerOptionSetup>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddScoped<IJwtProvider, JwtProvider>();


        services.AddScoped<IPermissionService, PermissionService>();
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorisationPolicyProvider>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<ISqlConnectionFactory>(_ =>
        new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }
}
