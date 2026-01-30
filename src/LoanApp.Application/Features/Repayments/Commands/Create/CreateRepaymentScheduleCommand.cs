using LoanApp.Application.Abstractions.Handlers;
using LoanApp.Domain.Entities;

namespace LoanApp.Application.Features.Repayments.Commands.Create;

public record CreateRepaymentScheduleCommand(Guid LoanApplicationId, DateOnly StartDate) : ICommand<CreateRepaymentScheduleResult>;

public record CreateRepaymentScheduleResult(int CreatedItems);
