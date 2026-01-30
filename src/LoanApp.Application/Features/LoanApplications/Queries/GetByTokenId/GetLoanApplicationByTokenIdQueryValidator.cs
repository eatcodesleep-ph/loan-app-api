using FluentValidation;

namespace LoanApp.Application.Features.LoanApplications.Queries.GetByTokenId;

public class GetLoanApplicationByTokenIdQueryValidator : AbstractValidator<GetLoanApplicationByTokenIdQuery>
{
    public GetLoanApplicationByTokenIdQueryValidator()
    {
        RuleFor(x => x.IdentityToken)
            .NotEmpty().WithMessage("Identity Token is required.");
    }
}
