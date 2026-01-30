using Asp.Versioning;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.DTOs;
using LoanApp.Application.Features.Repayments.Commands.Create;

namespace LoanApp.Api.Endpoints;

public static class RepaymentEndpoint
{
    public static IEndpointRouteBuilder MapRepaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
                 .HasApiVersion(new ApiVersion(1, 0))
                 .ReportApiVersions()
                 .Build();

        var group = app.MapGroup("/repayment/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .WithTags("Repayment");

        group.MapPost("/generate", async (
            CreateRepaymentScheduleDto request,
            ICommandHandler<CreateRepaymentScheduleCommand, CreateRepaymentScheduleResult> handler,
            CancellationToken ct) =>
            {
                var command = new CreateRepaymentScheduleCommand(
                    request.LoanApplicationId,
                    request.StarDate);

                var result = await handler.Handle(command, ct);
                return result.IsSuccess ? Results.Created(result.Value?.CreatedItems.ToString(), result.Value) : Results.ValidationProblem(result.ValidationErrors!);
            })
            .RequireAuthorization("ApiWrite");

        return app;
    }
}