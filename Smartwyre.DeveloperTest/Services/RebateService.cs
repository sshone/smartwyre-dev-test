using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateStrategyFactory _rebateStrategyFactory;

    public RebateService( IRebateDataStore rebateDataStore, 
                          IProductDataStore productDataStore,
                          IRebateStrategyFactory rebateStrategyFactory)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateStrategyFactory = rebateStrategyFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        try
        {
            var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            var product = _productDataStore.GetProduct(request.ProductIdentifier);

            if (rebate == null || product == null)
            {
                return new CalculateRebateResult
                {
                    Success = false
                };
            }

            var rebateStrategy = _rebateStrategyFactory.GetStrategy(rebate.Incentive);

            if (rebateStrategy == null || !rebateStrategy.CanCalculate(rebate, product, request))
            {
                return new CalculateRebateResult
                {
                    Success = false
                };
            }

            var rebateAmount = rebateStrategy.CalculateRebate(rebate, product, request);
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

            return new CalculateRebateResult
            {
                Success = true
            };
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);

            return new CalculateRebateResult
            {
                Success = false
            };
        }
    }
}