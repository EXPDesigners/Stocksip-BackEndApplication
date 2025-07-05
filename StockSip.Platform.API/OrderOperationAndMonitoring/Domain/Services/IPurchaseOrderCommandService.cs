using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;

public interface IPurchaseOrderCommandService
{
    Task<PurchaseOrder?> HandleAsync(CreateOrderCommand command);
    
    Task ChangeStatusAsync(long orderId, OrderStatus newStatus);
}