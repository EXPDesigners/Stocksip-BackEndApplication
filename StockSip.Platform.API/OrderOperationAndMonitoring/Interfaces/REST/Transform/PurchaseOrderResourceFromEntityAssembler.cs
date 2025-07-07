using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public static class PurchaseOrderResourceFromEntityAssembler
{
    public static PurchaseOrderResource ToResourceFromEntity(PurchaseOrder po)
    {
        if (po is null) throw new ArgumentNullException(nameof(po));

        return new PurchaseOrderResource(
            Id: po.Id,
            Date: po.Date.Value, // DateTime directamente
            Status: po.Status.ToString(),
            Buyer: ToAccount(po.Buyer),
            Supplier: ToAccount(po.Supplier),
            Items: po.Items.Select(ToItem).ToList(),
            TotalAmount: po.TotalAmount,
            TotalItems: po.TotalItems
        );
    }

    private static AccountResource ToAccount(Buyer b) =>
        new(
            AccountId: b.AccountId,
            UserOwnerId: b.UserOwnerId,
            Role: b.Role,
            BusinessName: b.BusinessName,
            Email: b.Email
        );

    private static AccountResource ToAccount(Supplier s) =>
        new(
            AccountId: s.AccountId,
            UserOwnerId: s.UserOwnerId,
            Role: s.Role,
            BusinessName: s.BusinessName,
            Email: s.Email
        );

    private static OrderItemResource ToItem(OrderItem i) =>
        new(
            Id: i.Id.ToString(),
            CatalogId: i.CatalogId,
            Name: i.Name,
            ProductType: i.ProductType,
            Brand: i.Brand,
            Content: i.Content,
            UnitPrice: (decimal)i.UnitPrice,
            DateAdded: i.DateAdded,
            CustomQuantity: i.CustomQuantity
        );
}