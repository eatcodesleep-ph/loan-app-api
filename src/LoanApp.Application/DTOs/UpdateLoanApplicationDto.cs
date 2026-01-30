namespace LoanApp.Application.DTOs;

public record UpdateLoanApplicationDto(
    string IdentityToken,
    decimal AmountRequired,
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
    string ProductType
);
