using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;

public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
{
    Task<IEnumerable<PurchaseOrder>> FindByOrderIdAsync(long orderId);
    
    /// <summary>
    /// Returns the purchase orders where the buyer belongs to the specified account.
    /// </summary>
    Task<IEnumerable<PurchaseOrder>> FindByBuyerAccountIdAsync(string accountId);

    /// <summary>
    /// Returns the purchase orders where the supplier belongs to the specified account.
    /// </summary>
    Task<IEnumerable<PurchaseOrder>> FindBySupplierAccountIdAsync(string accountId);
}