using LoanApp.Application.Abstractions.Data;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LoanApp.Application.Features.LoanApplications.Commands.Update;

public class UpdateLoanApplicationCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateLoanApplicationCommand, UpdateLoanApplicationResult>
{
    public async Task<Result<UpdateLoanApplicationResult>> Handle(UpdateLoanApplicationCommand request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var loanApplication = await dbContext.LoanApplications
            .FirstOrDefaultAsync(x => x.IdentityToken == request.IdentityToken, ct);

        if(loanApplication is null) return Result<UpdateLoanApplicationResult>.Failure(new Error("Not Found", "Loan Application not found"));

        loanApplication.Title = request.Title;
        loanApplication.FirstName = request.FirstName;
        loanApplication.LastName = request.LastName;
        loanApplication.DateOfBirth = request.DateOfBirth;
        loanApplication.Mobile = request.Mobile;
        loanApplication.Email = request.Email;
        loanApplication.Amount = request.Amount;
        loanApplication.Term = request.Term;
        loanApplication.RepaymentAmount = request.RepaymentAmount;
        loanApplication.EstablishmentFee = request.EstablishmentFee;
        loanApplication.TotalInterest = request.TotalInterest;
        loanApplication.ProductType = request.ProductType;

        await dbContext.SaveChangesAsync(ct);

        return new UpdateLoanApplicationResult(loanApplication.IdentityToken!);
    }
}
