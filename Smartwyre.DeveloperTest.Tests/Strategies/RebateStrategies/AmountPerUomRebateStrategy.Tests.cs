using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Strategies.RebateStrategies;

public class AmountPerUomRebateStrategyTests
{
    private readonly AmountPerUomRebateStrategy strategy;

    public AmountPerUomRebateStrategyTests()
    {
        strategy = new AmountPerUomRebateStrategy();
    }

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount, false)]
    [InlineData(IncentiveType.FixedRateRebate, false)]
    [InlineData(IncentiveType.AmountPerUom, true)]
    public void AmountPerUomRebateStrategy_CanHandle_ReturnsCorrectResult(IncentiveType incentiveType, bool expectedResult)
    {
        // Act
        var result = strategy.CanHandle(incentiveType);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CanCalculate_ReturnsTrueForValidRebateAndProduct()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CanCalculate_ReturnsFalseWhenRebateIsNull()
    {
        // Arrange
        var rebate = (Rebate)null;
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CanCalculate_ReturnsFalseWhenProductIsNull()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = (Product)null;
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CanCalculate_ReturnsFalseWhenIncentiveNotSupported()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CanCalculate_ReturnsFalseWhenRebateAmountIsZero()
    {
        // Arrange
        var rebate = new Rebate { Amount = 0 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CanCalculate_ReturnsFalseWhenRequestVolumeIsZero()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 0 };

        // Act
        var result = strategy.CanCalculate(rebate, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AmountPerUomRebateStrategy_CalculateRebate_ReturnsCorrectRebateAmount()
    {
        // Arrange
        var rebate = new Rebate { Amount = 10 };
        var product = new Product();
        var request = new CalculateRebateRequest { Volume = 5 };

        // Act
        var result = strategy.CalculateRebate(rebate, product, request);

        // Assert
        Assert.Equal(50, result);
    }
}
