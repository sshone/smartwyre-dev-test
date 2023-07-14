using Moq;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Factories;
using System.Collections.Generic;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Factories;

public class RebateStrategyFactoryTests
{
    private readonly RebateStrategyFactory factory;
    private readonly Mock<IRebateStrategy> strategyMock1;
    private readonly Mock<IRebateStrategy> strategyMock2;
    private readonly Mock<IRebateStrategy> strategyMock3;

    public RebateStrategyFactoryTests()
    {
        strategyMock1 = new Mock<IRebateStrategy>();
        strategyMock2 = new Mock<IRebateStrategy>();
        strategyMock3 = new Mock<IRebateStrategy>();

        factory = new RebateStrategyFactory(new List<IRebateStrategy>
        {
            strategyMock1.Object,
            strategyMock2.Object,
            strategyMock3.Object
        });
    }

    [Fact]
    public void RebateStrategyFactory_GetStrategy_ReturnsMatchingStrategyForIncentiveType()
    {
        // Arrange
        var incentiveType = IncentiveType.FixedCashAmount;
        strategyMock1.Setup(s => s.CanHandle(incentiveType)).Returns(false);
        strategyMock2.Setup(s => s.CanHandle(incentiveType)).Returns(true);
        strategyMock3.Setup(s => s.CanHandle(incentiveType)).Returns(false);

        // Act
        var result = factory.GetStrategy(incentiveType);

        // Assert
        Assert.Equal(strategyMock2.Object, result);
    }

    [Fact]
    public void RebateStrategyFactory_GetStrategy_ReturnsNullWhenNoMatchingStrategyFound()
    {
        // Arrange
        var incentiveType = IncentiveType.AmountPerUom;
        strategyMock1.Setup(s => s.CanHandle(incentiveType)).Returns(false);
        strategyMock2.Setup(s => s.CanHandle(incentiveType)).Returns(false);
        strategyMock3.Setup(s => s.CanHandle(incentiveType)).Returns(false);

        // Act
        var result = factory.GetStrategy(incentiveType);

        // Assert
        Assert.Null(result);
    }
}
