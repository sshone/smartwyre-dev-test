using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IRebateDataStore
{
    /// <summary>
    /// Retrieves a rebate from the data store
    /// </summary>
    /// <param name="rebateIdentifier"></param>
    /// <returns></returns>
    Rebate GetRebate(string rebateIdentifier);

    /// <summary>
    /// Stores the result of a rebate calculation
    /// </summary>
    /// <param name="account"></param>
    /// <param name="rebateAmount"></param>
    void StoreCalculationResult(Rebate account, decimal rebateAmount);
}
