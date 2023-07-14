using Smartwyre.DeveloperTest.Types;

public class AmountPerUomRebateStrategy : IRebateStrategy
{
    public bool CanHandle(IncentiveType incentiveType)
    {
        return incentiveType == IncentiveType.AmountPerUom;
    }

    public bool CanCalculate(Rebate rebate, Product product,CalculateRebateRequest request)
    {
          return rebate != null && 
                 product != null && 
                 product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) && 
                 rebate.Amount != 0 && 
                 request.Volume != 0;
    }

    public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount * request.Volume;
    }
}