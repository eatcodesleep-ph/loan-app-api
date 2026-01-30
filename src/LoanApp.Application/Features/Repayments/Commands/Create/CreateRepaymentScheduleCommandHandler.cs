using LoanApp.Application.Abstractions.Data;
using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Common;
using LoanApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanApp.Application.Features.Repayments.Commands.Create;

public class CreateRepaymentScheduleCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateRepaymentScheduleCommand, CreateRepaymentScheduleResult>
{
    public async Task<Result<CreateRepaymentScheduleResult>> Handle(CreateRepaymentScheduleCommand request, CancellationToken ct)
    {
        var loanApplication = await dbContext.LoanApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(la => la.Id == request.LoanApplicationId, ct);

        if (loanApplication is null) return Result<CreateRepaymentScheduleResult>.Failure(new Error("NotFound", "Loan Application not found"));

        var term = loanApplication.Term;
        var createdItems = 0;
        var dueDate = request.StartDate;

        for (int i = 1; i <= term; i++)
        {
            var item = RepaymentSchedule.Create(loanApplication.Id, i, dueDate, loanApplication.RepaymentAmount, loanApplication.Amount);
            await dbContext.RepaymentSchedules.AddAsync(item, ct);
            createdItems++;

            dueDate = dueDate.AddMonths(1);
        }

        await dbContext.SaveChangesAsync(ct); 
        return new CreateRepaymentScheduleResult(createdItems);
    }
}
