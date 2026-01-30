using LoanApp.Application.Abstractions.Handlers;

namespace LoanApp.Application.Features.LoanApplications.Queries.GetByTokenId;

public record GetLoanApplicationByTokenIdQuery(string IdentityToken) : IQuery<GetLoanApplicationByTokenIdResult>;

public record GetLoanApplicationByTokenIdResult(
    decimal Amount,
    int Term,
    string Title,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Mobile,
    string Email,
    decimal? RepaymentAmount,
    decimal? EstablishmentFee,
    decimal? TotalInterest,
    string? Status);