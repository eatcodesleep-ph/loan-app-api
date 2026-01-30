using Asp.Versioning;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.DTOs;
using LoanApp.Application.Features.Authentication.CreateAccessToken;
using LoanApp.Infrastructure.Services;

namespace LoanApp.Api.Endpoints;

public static class AuthenticationEndpoint
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("/authentication/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .WithTags("Authentication");

        group.MapPost("/", async (
            TokenRequestDto request,
            IClientStore clients,
            ICommandHandler <CreateAccessTokenCommand, CreateAccessTokenResult> handler,
            CancellationToken ct) =>
        {
            if (!string.Equals(request.GrantType, "client_credentials", StringComparison.Ordinal))
                return Results.BadRequest(new { error = "unsupported_grant_type" });

            if (string.IsNullOrWhiteSpace(request.ClientId) || string.IsNullOrWhiteSpace(request.ClientSecret))
                return Results.BadRequest(new { error = "invalid_client" });

            var client = await clients.FindAsync(request.ClientId);
            if (client is null || !string.Equals(request.ClientSecret, client.SecretHash)) return Results.Unauthorized();

            var requestedScopes = string.IsNullOrWhiteSpace(request.Scope) ? [] : request.Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (!requestedScopes.All(s => client.AllowedScopes.Contains(s)))
                return Results.BadRequest(new { error = "invalid_scope" });

            var command = new CreateAccessTokenCommand(request.GrantType, request.ClientId, request.ClientSecret, requestedScopes);

            var result = await handler.Handle(command, ct);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.ValidationProblem(result.ValidationErrors!);
        }).AllowAnonymous();

        return app;
    }
}
