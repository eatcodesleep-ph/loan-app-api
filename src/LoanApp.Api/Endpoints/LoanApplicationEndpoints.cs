using Asp.Versioning;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.DTOs;
using LoanApp.Application.Features.LoanApplications.Commands.Create;
using LoanApp.Application.Features.LoanApplications.Commands.Update;
using LoanApp.Application.Features.LoanApplications.Queries.GetByTokenId;

namespace LoanApp.Api.Endpoints;

public static class LoanApplicationEndpoints
{
    public static IEndpointRouteBuilder MapLoanApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("/loanApplication/v{version:apiVersion}")
            .WithApiVersionSet(versionSet).WithTags("Loan Application");

        group.MapPost("/", async (
            CreateLoanApplicationDto request,
            ICommandHandler<CreateLoanApplicationCommand, CreateLoanApplicationResult> handler,
            CancellationToken ct) =>
            {
                var command = new CreateLoanApplicationCommand(
                    request.AmountRequired,
                    request.Term,
                    request.Title,
                    request.FirstName,
                    request.LastName,
                    request.DateOfBirth,
                    request.Mobile,
                    request.Email);

                var result = await handler.Handle(command, ct);
                return result.IsSuccess ? Results.Created(result.Value?.RedirectUrl, result.Value) : Results.ValidationProblem(result.ValidationErrors!);
            })
            .RequireAuthorization("ApiWrite");

        group.MapPut("/", async (
            UpdateLoanApplicationDto request,
            ICommandHandler<UpdateLoanApplicationCommand, UpdateLoanApplicationResult> handler,
            CancellationToken ct) =>
            {
                var command = new UpdateLoanApplicationCommand(
                    request.IdentityToken,
                    request.AmountRequired,
                    request.Term,
                    request.Title,
                    request.FirstName,
                    request.LastName,
                    request.DateOfBirth,
                    request.Mobile,
                    request.Email,
                    request.RepaymentAmount,
                    request.EstablishmentFee,
                    request.TotalInterest,
                    request.ProductType);

                var result = await handler.Handle(command, ct);
                return result.IsSuccess ? Results.Created(result.Value?.IdentityToken, result.Value) : Results.ValidationProblem(result.ValidationErrors!);

            })
            .RequireAuthorization("ApiWrite");

        group.MapGet("/{identityToken}", async (
            string identityToken,
            IQueryHandler<GetLoanApplicationByTokenIdQuery, GetLoanApplicationByTokenIdResult> handler,
            CancellationToken ct) =>
            {
                var query = new GetLoanApplicationByTokenIdQuery(identityToken);

                var result = await handler.Handle(query, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.ValidationProblem(result.ValidationErrors!);
            })
            .RequireAuthorization("ApiRead");

        return app;
    }
}
