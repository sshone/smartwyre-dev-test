using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Strategies.RebateStrategies;
public class FixedCashAmountRebateStrategyTests
{
    private readonly FixedCashAmountRebateStrategy strategy;

    public FixedCashAmountRebateStrategyTests()
    {
        strategy = new FixedCashAmountRebateStrategy();
    }

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount, true)]
    [InlineData(IncentiveType.FixedRateRebate, false)]
    [InlineData(IncentiveType.AmountPerUom, false)]
    public void FixedCashAmountRebateStrategy_CanHandle_ReturnsCorrectResult(IncentiveType incentiveType, bool expectedResult)
    {
        // Act
        var result = strategy.CanHandle(incentiveType);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void FixedCashAmountRebateStrategy_CanCalculate_ReturnsTrueForValidRebateAndProduct()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest();

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void FixedCashAmountRebateStrategy_CanCalculate_ReturnsFalseWhenRebateIsNull()
    {
        // Arrange
        var rebate = null as Rebate;
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest();

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedCashAmountRebateStrategy_CanCalculate_ReturnsFalseWhenProductIsNull()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = null as Product;
        var request = new CalculateRebateRequest();

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedCashAmountRebateStrategy_CanCalculate_ReturnsFalseWhenIncentiveNotSupported()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
        var request = new CalculateRebateRequest();

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedCashAmountRebateStrategy_CanCalculate_ReturnsFalseWhenRebateAmountIsZero()
    {
        // Arrange
        var rebate = new Rebate { Amount = 0 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest();

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedCashAmountRebateStrategy_CalculateRebate_ReturnsCorrectRebateAmount()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product();
        var request = new CalculateRebateRequest();

        // Act
        var result = strategy.CalculateRebate(rebate, product, request);

        // Assert
        Assert.Equal(10, result);
    }
}
