using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

public record CreateOrderCommand(
    Buyer               Buyer,
    Supplier            Supplier,
    IReadOnlyList<OrderItem> Items,
    decimal             TotalAmount,
    int                 TotalItems);