using LoanApp.Application.Abstractions.Handlers;

namespace LoanApp.Application.Features.LoanApplications.Commands.Create;

public record CreateLoanApplicationCommand(
    decimal Amount,
    int Term,
    string Title,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Mobile,
    string Email) : ICommand<CreateLoanApplicationResult>;

public record CreateLoanApplicationResult(string RedirectUrl);