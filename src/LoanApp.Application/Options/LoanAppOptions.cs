namespace LoanApp.Application.Options;

public class LoanAppOptions
{
    public string? RedirectBaseUrl { get; set; }
    public decimal EstablishmentFee { get; set; }
    public decimal MonthlyInterestRate { get; set; }
    public decimal MinimumLoanAmount { get; set; }
    public decimal MaxLoanAmount { get; set; }
    public int MinimumAge { get; set; }
    public int MaxTermInMonths { get; set; }
    public List<string>? BlackListedEmailDomains { get; set; }
    public List<string>? BlackListedMobileNumbers { get; set; }
}
