using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Strategies.RebateStrategies;

public class FixedRateRebateStrategyTests
{
    private readonly FixedRateRebateStrategy strategy;

    public FixedRateRebateStrategyTests()
    {
        strategy = new FixedRateRebateStrategy();
    }

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount, false)]
    [InlineData(IncentiveType.FixedRateRebate, true)]
    [InlineData(IncentiveType.AmountPerUom, false)]
    public void FixedRateRebateStrategy_CanHandle_ReturnsCorrectResult(IncentiveType incentiveType, bool expectedResult)
    {
        // Act
        var result = strategy.CanHandle(incentiveType);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsTrueForValidRebateAndProduct()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsFalseWhenRebateIsNull()
    {
        // Arrange
        var rebate = null as Rebate;
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsFalseWhenProductIsNull()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = null as Product;
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsFalseWhenIncentiveNotSupported()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsFalseWhenRebatePercentageIsZero()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsFalseWhenProductPriceIsZero()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 0 };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CanCalculate_ReturnsFalseWhenRequestVolumeIsZero()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100 };
        var request = new CalculateRebateRequest { Volume = 0 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FixedRateRebateStrategy_CalculateRebate_ReturnsCorrectRebateAmount()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { Price = 100 };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CalculateRebate(rebate, product, request);

        // Assert
        Assert.Equal(50, result);
    }
}
