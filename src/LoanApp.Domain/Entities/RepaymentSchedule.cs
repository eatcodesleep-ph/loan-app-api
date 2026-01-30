namespace LoanApp.Domain.Entities;

public class RepaymentSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid LoanApplicationId { get; set; }
    public int InstallmentNumber { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal? RepaymentAmount { get; set; }
    public decimal? Principal { get; set; }
    public decimal? Interest { get; set; }
    public decimal? PaidAmount { get; set; }
    public bool? IsPaid { get; set; }
    public static RepaymentSchedule Create(Guid loanApplicationId, int installmentNumber, DateOnly dueDate, decimal? repaymentAmount, decimal? principal) =>
        new() {
            LoanApplicationId = loanApplicationId,
            InstallmentNumber = installmentNumber,
            DueDate = dueDate,
            RepaymentAmount = repaymentAmount,
            Principal = principal
        };
}


