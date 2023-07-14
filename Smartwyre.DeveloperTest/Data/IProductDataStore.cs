using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IProductDataStore
{
    /// <summary>
    /// Retrieves a product from the data store
    /// </summary>
    /// <param name="productIdentifier"></param>
    /// <returns></returns>
    Product GetProduct(string productIdentifier);
}
