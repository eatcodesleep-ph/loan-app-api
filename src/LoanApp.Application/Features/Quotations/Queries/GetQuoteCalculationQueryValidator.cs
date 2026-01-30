using FluentValidation;

namespace LoanApp.Application.Features.Quotations.Queries;

public class GetQuoteCalculationQueryValidator : AbstractValidator<GetQuoteCalculationQuery>
{
    public GetQuoteCalculationQueryValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Term).InclusiveBetween(1, 600);
    }
}
