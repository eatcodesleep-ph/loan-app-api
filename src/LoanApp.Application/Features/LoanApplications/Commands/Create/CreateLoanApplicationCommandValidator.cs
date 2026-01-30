using FluentValidation;
using LoanApp.Application.Common;
using LoanApp.Application.Options;
using Microsoft.Extensions.Options;

namespace LoanApp.Application.Features.LoanApplications.Commands.Create;

public class CreateLoanApplicationCommandValidator : AbstractValidator<CreateLoanApplicationCommand>
{
    public CreateLoanApplicationCommandValidator(IOptions<LoanAppOptions> options)
    {
        var maxTermInMonths = options.Value.MaxTermInMonths;
        var maxLoanAmount = options.Value.MaxLoanAmount;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(Constants.MaxTitleLength).WithMessage($"Title must be at most {Constants.MaxTitleLength} characters.")
            .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("Title cannot be whitespace.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(Constants.MaxNameLength).WithMessage($"First name must be at most {Constants.MaxNameLength} characters.")
            .Matches(@"^[\p{L} \-']+$").WithMessage("First name contains invalid characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(Constants.MaxNameLength).WithMessage($"Last name must be at most {Constants.MaxNameLength} characters.")
            .Matches(@"^[\p{L} \-']+$").WithMessage("Last name contains invalid characters.");

        RuleFor(x => x.DateOfBirth)
            .NotNull().WithMessage("Date of birth is required and must be a valid date.")
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Mobile is required.")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Mobile must be a valid phone number (7-15 digits, optional leading +).");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than zero.")
            .Must(a => a <= maxLoanAmount).WithMessage($"Amount must be less than or equal to {maxLoanAmount:N0}.");

        RuleFor(x => x.Term)
            .NotEmpty().WithMessage("Term is required.")
            .GreaterThan(0).WithMessage("Term must be greater than zero.")
            .Must(t => t <= maxTermInMonths).WithMessage($"Term must be less than or equal to {maxTermInMonths} months.");
    }
}
