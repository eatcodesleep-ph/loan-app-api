using LoanApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanApp.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<LoanApplication> LoanApplications { get; }
    DbSet<RepaymentSchedule> RepaymentSchedules { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
