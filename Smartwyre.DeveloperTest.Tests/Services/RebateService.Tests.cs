using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateServiceTests
{
    private readonly Mock<IRebateDataStore> rebateDataStoreMock;
    private readonly Mock<IProductDataStore> productDataStoreMock;
    private readonly Mock<IRebateStrategyFactory> rebateStrategyFactoryMock;
    private readonly RebateService rebateService;

    public RebateServiceTests()
    {
        rebateDataStoreMock = new Mock<IRebateDataStore>();
        productDataStoreMock = new Mock<IProductDataStore>();
        rebateStrategyFactoryMock = new Mock<IRebateStrategyFactory>();
        rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, rebateStrategyFactoryMock.Object);
    }

    [Fact]
    public void RebateService_Calculate_ReturnsSuccessResultWhenRebateAndProductExistAndCanCalculate()
    {
        // Arrange
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1"
        };
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var rebateStrategyMock = new Mock<IRebateStrategy>();
        rebateStrategyMock.Setup(s => s.CanCalculate(rebate, product, request)).Returns(true);
        rebateStrategyFactoryMock.Setup(f => f.GetStrategy(rebate.Incentive)).Returns(rebateStrategyMock.Object);
        rebateDataStoreMock.Setup(d => d.GetRebate(request.RebateIdentifier)).Returns(rebate);
        productDataStoreMock.Setup(d => d.GetProduct(request.ProductIdentifier)).Returns(product);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void RebateService_Calculate_ReturnsFailureResultWhenRebateIsNull()
    {
        // Arrange
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1"
        };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var rebateStrategyMock = new Mock<IRebateStrategy>();
        rebateStrategyFactoryMock.Setup(f => f.GetStrategy(It.IsAny<IncentiveType>())).Returns(rebateStrategyMock.Object);
        rebateDataStoreMock.Setup(d => d.GetRebate(request.RebateIdentifier)).Returns((Rebate)null);
        productDataStoreMock.Setup(d => d.GetProduct(request.ProductIdentifier)).Returns(product);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void RebateService_Calculate_ReturnsFailureResultWhenProductIsNull()
    {
        // Arrange
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1"
        };
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var rebateStrategyMock = new Mock<IRebateStrategy>();
        rebateStrategyFactoryMock.Setup(f => f.GetStrategy(rebate.Incentive)).Returns(rebateStrategyMock.Object);
        rebateDataStoreMock.Setup(d => d.GetRebate(request.RebateIdentifier)).Returns(rebate);
        productDataStoreMock.Setup(d => d.GetProduct(request.ProductIdentifier)).Returns((Product)null);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void RebateService_Calculate_ReturnsFailureResultWhenRebateStrategyIsNull()
    {
        // Arrange
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1"
        };
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        rebateStrategyFactoryMock.Setup(f => f.GetStrategy(rebate.Incentive)).Returns((IRebateStrategy)null);
        rebateDataStoreMock.Setup(d => d.GetRebate(request.RebateIdentifier)).Returns(rebate);
        productDataStoreMock.Setup(d => d.GetProduct(request.ProductIdentifier)).Returns(new Product());

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void RebateService_Calculate_ReturnsFailureResultWhenRebateStrategyCannotCalculate()
    {
        // Arrange
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1"
        };
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var rebateStrategyMock = new Mock<IRebateStrategy>();
        rebateStrategyMock.Setup(s => s.CanCalculate(rebate, product, request)).Returns(false);
        rebateStrategyFactoryMock.Setup(f => f.GetStrategy(rebate.Incentive)).Returns(rebateStrategyMock.Object);
        rebateDataStoreMock.Setup(d => d.GetRebate(request.RebateIdentifier)).Returns(rebate);
        productDataStoreMock.Setup(d => d.GetProduct(request.ProductIdentifier)).Returns(product);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void RebateService_Calculate_CallsRebateDataStoreToStoreCalculationResultWhenSuccessful()
    {
        // Arrange
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1"
        };
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var rebateStrategyMock = new Mock<IRebateStrategy>();
        rebateStrategyMock.Setup(s => s.CanCalculate(rebate, product, request)).Returns(true);
        rebateStrategyMock.Setup(s => s.CalculateRebate(rebate, product, request)).Returns(10);
        rebateStrategyFactoryMock.Setup(f => f.GetStrategy(rebate.Incentive)).Returns(rebateStrategyMock.Object);
        rebateDataStoreMock.Setup(d => d.GetRebate(request.RebateIdentifier)).Returns(rebate);
        productDataStoreMock.Setup(d => d.GetProduct(request.ProductIdentifier)).Returns(product);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        rebateDataStoreMock.Verify(d => d.StoreCalculationResult(rebate, 10), Times.Once);
    }
}
