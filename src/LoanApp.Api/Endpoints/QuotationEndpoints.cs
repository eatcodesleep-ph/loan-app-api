using Asp.Versioning;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.Features.Quotations.Queries;

namespace LoanApp.Api.Endpoints;

public static class QuotationEndpoints
{
    public static IEndpointRouteBuilder MapQuotationEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("/quotation/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .WithTags("Quotation");

        group.MapGet("/calculate", async (
            decimal amount,
            int term,
            string product,
            IQueryHandler<GetQuoteCalculationQuery, GetQuoteCalculationResult> handler,
            CancellationToken ct) =>
            {
                var query = new GetQuoteCalculationQuery(amount, term, product);

                var result = await handler.Handle(query, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.ValidationProblem(result.ValidationErrors!);
            })
            .RequireAuthorization("ApiRead");

        return app;
    }
}
