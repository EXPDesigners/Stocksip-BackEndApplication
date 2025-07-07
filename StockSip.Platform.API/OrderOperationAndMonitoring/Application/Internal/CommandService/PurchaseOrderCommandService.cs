using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Application.Internal.CommandService;

public class PurchaseOrderCommandService(
    IPurchaseOrderRepository orderRepository,
    IUnitOfWork             unitOfWork) : IPurchaseOrderCommandService
{
    public async Task<PurchaseOrder> HandleAsync(CreateOrderCommand cmd)
    {
        var order = new PurchaseOrder(cmd);
        await orderRepository.AddAsync(order);
        await unitOfWork.CompleteAsync();
        return order;
    }
    
    public async Task ChangeStatusAsync(long orderId, OrderStatus newStatus)
    {
        var order = (await orderRepository.FindByOrderIdAsync(orderId))
                    .FirstOrDefault()                               // ← selecciona la única orden
                    ?? throw new ArgumentException($"Order {orderId} not found.");

        order.ChangeStatus(newStatus);
        await unitOfWork.CompleteAsync();
    }

}