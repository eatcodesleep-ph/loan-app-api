using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Application.Options;
using LoanApp.Domain.Common;
using LoanApp.Domain.Enums;
using Microsoft.Extensions.Options;

namespace LoanApp.Application.Features.Quotations.Queries;

public class GetQuoteCalculationQueryHandler(IOptions<LoanAppOptions> options) : IQueryHandler<GetQuoteCalculationQuery, GetQuoteCalculationResult>
{
    public async Task<Result<GetQuoteCalculationResult>> Handle(GetQuoteCalculationQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var monthlyInterestRate = options.Value.MonthlyInterestRate;
        var establishmentFee = options.Value.EstablishmentFee;
        var termInMonths = request.Term;
        var presentValue = request.Amount;
        var product = request.product;

        var repayment = PmtCalculation(monthlyInterestRate, termInMonths, presentValue, product);
        repayment = Round(repayment);

        var totalPayment = repayment * termInMonths + (establishmentFee);
        totalPayment = Round(totalPayment);

        var totalInterest = totalPayment - request.Amount - establishmentFee;
        totalInterest = Round(totalInterest);

        var totalRepayment = presentValue + totalInterest;
        return new GetQuoteCalculationResult(totalRepayment, repayment, establishmentFee, totalInterest);
    }

    private static decimal PmtCalculation(decimal monthlyRate, int termMonths, decimal presentValue, string product)
    {
        if (product == ProductType.ProductA.ToString())
        {
            monthlyRate = 0;
        }
        else if (product == ProductType.ProductB.ToString())
        {
            termMonths -= 2;
        }

        if (monthlyRate == 0)
        {
            return presentValue / termMonths;
        }

        double r = (double)monthlyRate;
        double n = termMonths;
        double pv = (double)presentValue;

        double factor = Math.Pow(1 + r, -n);
        double payment = r * pv / (1 - factor);

        return (decimal)payment;
    }

    private static decimal Round(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.ToEven);
    }
}
