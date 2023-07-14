using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Factories;

public class RebateStrategyFactory : IRebateStrategyFactory
{
    private readonly IEnumerable<IRebateStrategy> _rebateStrategies;

    public RebateStrategyFactory(IEnumerable<IRebateStrategy> rebateStrategies)
    {
        _rebateStrategies = rebateStrategies;
    }

    public IRebateStrategy GetStrategy(IncentiveType incentiveType)
    {
        return _rebateStrategies.FirstOrDefault(s => s.CanHandle(incentiveType));
    }
}
