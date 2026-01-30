namespace LoanApp.Application.DTOs;

public record CreateRepaymentScheduleDto(Guid LoanApplicationId, DateOnly StarDate);
