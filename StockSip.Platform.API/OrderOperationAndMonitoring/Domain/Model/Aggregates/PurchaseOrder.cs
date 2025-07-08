using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;

public class PurchaseOrder
{
    public long Id { get; private set; }
    
    public OrderDate Date { get; private set; } = default!;
    
    public OrderStatus Status { get; private set; }

    public void ChangeStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Canceled)
            throw new InvalidOperationException("Cannot move from CANCELED.");
        Status = newStatus;
    }
    
    public Buyer Buyer { get; private set; } = default!;
    
    public Supplier Supplier { get; private set; } = default!;
    
    public IList<OrderItem> Items { get; private set; } = new List<OrderItem>();

    public decimal TotalAmount { get; private set; }
    public int     TotalItems  { get; private set; }
    
    private PurchaseOrder() { }

    public PurchaseOrder(CreateOrderCommand cmd)
    {
        Date        = new OrderDate(DateTime.UtcNow);
        Status      = OrderStatus.Received;
        Buyer       = cmd.Buyer;
        Supplier    = cmd.Supplier;
        Items       = cmd.Items.ToList();
        TotalAmount = cmd.TotalAmount;
        TotalItems  = cmd.TotalItems;
    }
}