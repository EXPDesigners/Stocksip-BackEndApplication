using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public class CreateOrderCommandFromResourceAssembler
{
    public static CreateOrderCommand ToCommandFromResource(CreateOrderResource r)
    {
        var buyer = new Buyer(
            r.Buyer.AccountId,
            r.Buyer.UserOwnerId,
            r.Buyer.Role,
            r.Buyer.BusinessName,
            r.Buyer.Email);
        
        var supplier = new Supplier(
            r.Supplier.AccountId,
            r.Supplier.UserOwnerId,
            r.Supplier.Role,
            r.Supplier.BusinessName,
            r.Supplier.Email);
        
        var items = r.Items.Select(it => new OrderItem(
                Guid.Parse(it.Id),
                it.CatalogId,
                it.Name,
                it.ProductType,
                it.Brand,
                it.Content,
                (decimal)it.UnitPrice,
                it.DateAdded ?? DateTime.UtcNow,
                it.CustomQuantity))
            .ToList();

        return new CreateOrderCommand(
            buyer,
            supplier,
            items,
            r.TotalAmount,
            r.TotalItems);
    }
}