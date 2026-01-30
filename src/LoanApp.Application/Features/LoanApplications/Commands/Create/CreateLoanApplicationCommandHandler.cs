using LoanApp.Application.Abstractions.Authentication;
using LoanApp.Application.Abstractions.Data;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.Options;
using LoanApp.Domain.Common;
using LoanApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoanApp.Application.Features.LoanApplications.Commands.Create;

public class CreateLoanApplicationCommandHandler(
    IApplicationDbContext dbContext,
    ITokenService tokenService,
    IOptions<LoanAppOptions> options) : ICommandHandler<CreateLoanApplicationCommand, CreateLoanApplicationResult>
{
    public async Task<Result<CreateLoanApplicationResult>> Handle(CreateLoanApplicationCommand request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var firstName = request?.FirstName?.ToUpperInvariant();
        var lastName = request?.LastName?.ToUpperInvariant();
        var dateOfBirth = request?.DateOfBirth;
        var redirectUrl = $"{options.Value.RedirectBaseUrl}?reference=";
        
        var loanApplication = await dbContext.LoanApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(result => result.FirstName == firstName && result.LastName == lastName && result.DateOfBirth == dateOfBirth, ct);

        if (loanApplication is not null) return new CreateLoanApplicationResult($"{redirectUrl}{loanApplication.IdentityToken}");

        var identityKey = $"{firstName}-{lastName}-{dateOfBirth:yyyy-MM-dd}";
        var token = tokenService.Create(identityKey);
        var newLoanApplication = new LoanApplication
        {
            IdentityToken = token,
            Title = request?.Title?.ToUpperInvariant(),
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = request!.DateOfBirth,
            Mobile = request?.Mobile,
            Email = request?.Email?.ToUpperInvariant(),
            Amount = request!.Amount,
            Term = request.Term
        };

        dbContext.LoanApplications.Add(newLoanApplication);
        await dbContext.SaveChangesAsync(ct);

        return new CreateLoanApplicationResult($"{redirectUrl}{token}");
    }
}
