using LoanApp.Domain.Enums;

namespace LoanApp.Domain.Entities;
public class LoanApplication
{
    public Guid Id { get; set; }
    public string? IdentityToken { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int Term { get; set; }
    public decimal Amount { get; set; }
    public decimal? RepaymentAmount { get; set; }
    public decimal? EstablishmentFee { get; set; }
    public decimal? TotalInterest { get; set; }
    public LoanApplicationStatus Status { get; set; }
    public string? ProductType { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public DateTime DateCreated { get; set; }
}
