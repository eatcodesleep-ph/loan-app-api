using LoanApp.Application.Abstractions.Handlers;

namespace LoanApp.Application.Features.LoanApplications.Commands.Update;

public record UpdateLoanApplicationCommand(
    string IdentityToken,
    decimal Amount,
    int Term,
    string Title,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Mobile,
    string Email,
    decimal RepaymentAmount,
    decimal EstablishmentFee,
    decimal TotalInterest,
    string ProductType) : ICommand<UpdateLoanApplicationResult>;

public record UpdateLoanApplicationResult(string IdentityToken);