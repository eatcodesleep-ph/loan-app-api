namespace LoanApp.Application.DTOs;

public record CreateLoanApplicationDto(
    decimal AmountRequired,
    int Term,
    string Title,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Mobile,
    string Email
);
