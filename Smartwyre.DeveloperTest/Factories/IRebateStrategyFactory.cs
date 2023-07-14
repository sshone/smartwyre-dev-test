using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Factories;

public interface IRebateStrategyFactory
{
    /// <summary>
    /// Retrieves a rebate strategy from the factory for the given incentive type
    /// </summary>
    /// <param name="incentiveType"></param>
    /// <returns></returns>
    IRebateStrategy GetStrategy(IncentiveType incentiveType);
}
