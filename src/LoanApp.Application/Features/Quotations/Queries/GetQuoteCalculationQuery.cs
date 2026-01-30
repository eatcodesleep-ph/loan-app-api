using LoanApp.Application.Abstractions.Handlers;

namespace LoanApp.Application.Features.Quotations.Queries;

public record GetQuoteCalculationQuery(decimal Amount, int Term, string product) : IQuery<GetQuoteCalculationResult>;

public record GetQuoteCalculationResult(decimal TotalRepaymentAmount, decimal RepaymentAmount, decimal EstablishmentFee, decimal TotalInterest);