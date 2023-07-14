using Smartwyre.DeveloperTest.Types;

public class FixedCashAmountRebateStrategy : IRebateStrategy
{
    public bool CanHandle(IncentiveType incentiveType)
    {
        return incentiveType == IncentiveType.FixedCashAmount;
    }

    public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null && 
               product != null && 
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) && 
               rebate.Amount != 0;
    }

    public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount;
    }
}