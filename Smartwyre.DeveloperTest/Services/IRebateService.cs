using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    /// <summary>
    /// Performs a rebate calculation for the given request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
