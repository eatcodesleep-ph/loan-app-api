using LoanApp.Application.Abstractions.Data;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LoanApp.Application.Features.LoanApplications.Queries.GetByTokenId;

public class GetLoanApplicationByTokenIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetLoanApplicationByTokenIdQuery, GetLoanApplicationByTokenIdResult>
{
    public async Task<Result<GetLoanApplicationByTokenIdResult>> Handle(GetLoanApplicationByTokenIdQuery request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        var loanApplication = await dbContext.LoanApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(la => la.IdentityToken == request.IdentityToken, ct);

        if (loanApplication is null) return Result<GetLoanApplicationByTokenIdResult>.Failure(new Error("Not Found", "Loan Application not found"));

        return new GetLoanApplicationByTokenIdResult(
            loanApplication.Amount,
            loanApplication.Term,
            loanApplication.Title ?? string.Empty,
            loanApplication.FirstName ?? string.Empty,
            loanApplication.LastName ?? string.Empty,
            loanApplication.DateOfBirth,
            loanApplication.Mobile ?? string.Empty,
            loanApplication.Email ?? string.Empty,
            loanApplication.RepaymentAmount,
            loanApplication.EstablishmentFee,
            loanApplication.TotalInterest,
            loanApplication.Status.ToString());
    }
}
