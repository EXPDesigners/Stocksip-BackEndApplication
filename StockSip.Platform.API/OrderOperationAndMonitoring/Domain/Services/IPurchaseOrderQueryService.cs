using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;

public interface IPurchaseOrderQueryService
{
    Task<IEnumerable<PurchaseOrder>> FindAllAsync();

    Task<IEnumerable<PurchaseOrder>> FindByBuyerAccountIdAsync(string buyerAccountId);

    Task<IEnumerable<PurchaseOrder>> FindBySupplierAccountIdAsync(string supplierAccountId);
}