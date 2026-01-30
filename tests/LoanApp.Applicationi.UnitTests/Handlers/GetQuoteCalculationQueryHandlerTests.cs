using LoanApp.Application.Features.Quotations.Queries;
using LoanApp.Application.Options;
using Microsoft.Extensions.Options;
using Moq;

namespace LoanApp.Application.UnitTests.Handlers;

public class GetQuoteCalculationQueryHandlerTests
{
    [Theory]
    [InlineData("ProductA", 1000.00, 12, 0.03, 300.00, 83.33)]
    [InlineData("ProductB", 1000.00, 12, 0.03, 300.00, 117.23)]
    [InlineData("ProductC", 1000.00, 12, 0.03, 300.00, 100.46)]
    public async Task Handle_ReturnsExpectedResults_ForVariousProducts(
        string product,
        decimal amount, 
        int term,
        decimal monthlyRate,
        decimal establishmentFee,
        decimal expectedRepayment)
    {
        // Arrange
        var mockOptions = new Mock<IOptions<LoanAppOptions>>();
        mockOptions.Setup(o => o.Value).Returns(new LoanAppOptions
        {
            MonthlyInterestRate = monthlyRate,
            EstablishmentFee = establishmentFee
        });

        var handler = new GetQuoteCalculationQueryHandler(mockOptions.Object);
        var query = new GetQuoteCalculationQuery(amount, term, product);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        var value = result.Value!;

        Assert.Equal(expectedRepayment, value.RepaymentAmount);
    }

    [Theory]
    [InlineData(0.01, 50.00)]
    public async Task Handle_ThrowsArgumentNullException_WhenRequestIsNull(double monthlyRateDouble,
        double establishmentFeeDouble)
    {
        // Arrange
        decimal monthlyRate = (decimal)monthlyRateDouble;
        decimal establishmentFee = (decimal)establishmentFeeDouble;
        var mockOptions = new Mock<IOptions<LoanAppOptions>>();
        mockOptions.Setup(o => o.Value).Returns(new LoanAppOptions
        {
            MonthlyInterestRate = monthlyRate,
            EstablishmentFee = establishmentFee
        });

        var handler = new GetQuoteCalculationQueryHandler(mockOptions.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
    }
}
