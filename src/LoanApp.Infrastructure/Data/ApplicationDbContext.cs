using LoanApp.Application.Abstractions.Data;
using LoanApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LoanApp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<RepaymentSchedule> RepaymentSchedules => Set<RepaymentSchedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
