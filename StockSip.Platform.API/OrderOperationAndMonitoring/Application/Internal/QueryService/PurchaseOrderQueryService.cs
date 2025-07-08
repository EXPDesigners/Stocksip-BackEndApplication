using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Application.Internal.QueryService;

public class PurchaseOrderQueryService(IPurchaseOrderRepository orderRepository)
    : IPurchaseOrderQueryService
{
    public Task<IEnumerable<PurchaseOrder>> FindAllAsync() =>
        orderRepository.ListAsync();

    public Task<IEnumerable<PurchaseOrder>> FindByBuyerAccountIdAsync(string buyerAccountId) =>
        orderRepository.FindByBuyerAccountIdAsync(buyerAccountId);

    public Task<IEnumerable<PurchaseOrder>> FindBySupplierAccountIdAsync(string supplierAccountId) =>
        orderRepository.FindBySupplierAccountIdAsync(supplierAccountId);
}