using Smartwyre.DeveloperTest.Types;

public class FixedRateRebateStrategy : IRebateStrategy
{
    public bool CanHandle(IncentiveType incentiveType)
    {
        return incentiveType == IncentiveType.FixedRateRebate;
    }

    public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null && 
               product != null && 
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) && 
               rebate.Percentage != 0 && 
               request.Volume != 0 &&
               product.Price != 0;
    }

    public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return product.Price * rebate.Percentage * request.Volume;
    }
}