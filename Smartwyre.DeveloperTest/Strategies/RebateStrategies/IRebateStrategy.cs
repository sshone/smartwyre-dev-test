using Smartwyre.DeveloperTest.Types;

public interface IRebateStrategy
{
    /// <summary>
    /// Checks if the strategy can handle the given incentive type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool CanHandle(IncentiveType type);

    /// <summary>
    /// Checks if the strategy can calculate the rebate amount
    /// </summary>
    /// <param name="rebate"></param>
    /// <param name="product"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request);

    /// <summary>
    /// Calculates the rebate amount
    /// </summary>
    /// <param name="rebate"></param>
    /// <param name="product"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request);
}