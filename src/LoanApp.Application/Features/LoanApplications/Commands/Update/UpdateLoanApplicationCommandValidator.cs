using FluentValidation;
using LoanApp.Application.Common;
using LoanApp.Application.Options;
using Microsoft.Extensions.Options;

namespace LoanApp.Application.Features.LoanApplications.Commands.Update;

public class UpdateLoanApplicationCommandValidator : AbstractValidator<UpdateLoanApplicationCommand>
{
    public UpdateLoanApplicationCommandValidator(IOptions<LoanAppOptions> options)
    {
        var minimumAge = options.Value.MinimumAge;
        var maxTermInMonths = options.Value.MaxTermInMonths;
        var maxLoanAmount = options.Value.MaxLoanAmount;
        var blacklistedEmailDomains = options.Value.BlackListedEmailDomains ?? [];
        var blackListedMobileNumbers = options.Value.BlackListedMobileNumbers ?? [];

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
            .Must(d => BeAtLeastMinimumAge(d, minimumAge)).WithMessage($"Applicants must be at least {minimumAge} years old.")
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Mobile is required.")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Mobile must be a valid phone number (7-15 digits, optional leading +).")
            .Must(m => BeNotBlacklistedMobileNumber(m, blackListedMobileNumbers)).WithMessage("Mobile Number is blacklisted.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .Must(e => BeNotBlacklistedEmailDomain(e, blacklistedEmailDomains)).WithMessage("Personal email domains are not allowed. Please use your corporate email.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than zero.")
            .Must(a => a <= maxLoanAmount).WithMessage($"Amount must be less than or equal to {maxLoanAmount:N0}.");

        RuleFor(x => x.Term)
            .NotEmpty().WithMessage("Term is required.")
            .GreaterThan(0).WithMessage("Term must be greater than zero.")
            .Must(t => t <= maxTermInMonths).WithMessage($"Term must be less than or equal to {maxTermInMonths} months.");
    }

    private static bool BeAtLeastMinimumAge(DateOnly dateOfBirth, int minimumAge)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        return age >= minimumAge;
    }

    private static bool BeNotBlacklistedEmailDomain(string email, List<string> blacklistedEmailDomains)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var atPos = normalizedEmail.LastIndexOf('@');
        if (atPos < 0 || atPos == normalizedEmail.Length - 1) return false;

        var domain = normalizedEmail[atPos..];

        return !blacklistedEmailDomains.Contains(domain);
    }

    private static bool BeNotBlacklistedMobileNumber(string mobileNumber, List<string> blackListedMobileNumbers)
    {
        var normalizedMobileNumber = mobileNumber.Trim().ToLowerInvariant();
        return !blackListedMobileNumbers.Contains(normalizedMobileNumber);
    }
}