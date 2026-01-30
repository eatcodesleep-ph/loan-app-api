using LoanApp.Application.Abstractions.Authentication;
using LoanApp.Application.Abstractions.Data;
using LoanApp.Infrastructure.Data;
using LoanApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoanApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        var clientId = configuration["ClientCredential:ClientId"] ?? string.Empty;
        var clientSecret = configuration["ClientCredential:ClientSecret"] ?? string.Empty;
        services.AddSingleton<IClientStore>(sp =>
            new InMemoryClientStore(
            [
                new Client
                (
                    ClientId: clientId,
                    SecretHash: clientSecret,
                    AllowedScopes: ["api.read", "api.write"]
                )
            ])
        );

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(o =>
           {
               o.MapInboundClaims = false;
               o.RequireHttpsMetadata = false;
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Secret"]!)),
                   ValidateIssuer = true,
                   ValidIssuer = configuration["Auth:Issuer"],
                   ValidateAudience = true,
                   ValidAudience = configuration["Auth:Audience"],
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ClockSkew = TimeSpan.Zero
               };
           });

        services.AddAuthorizationBuilder()
            .AddPolicy("ApiRead", policy =>
                policy.RequireClaim("scp", "api.read"))
            .AddPolicy("ApiWrite", policy =>
                policy.RequireClaim("scp", "api.write"));

        services.AddSingleton<ITokenService, TokenService>();

        return services;
    }
}
