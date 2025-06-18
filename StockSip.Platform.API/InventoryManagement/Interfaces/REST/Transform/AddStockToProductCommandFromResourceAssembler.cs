using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an AddStockToProductResource into an AddStockToProductCommand.
/// </summary>
public static class AddStockToProductCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms an AddStockToProductResource into an AddStockToProductCommand.
    /// </summary>
    /// <param name="resource"> The AddStockToProductResource to transform. </param>
    /// <returns> The AddStockToProductCommand created from the resource. </returns>
    public static AddStockToProductCommand ToCommandFromResource(AddStockToProductResource resource)
    {
        return new AddStockToProductCommand(resource.ProductId, 
            resource.WarehouseId, 
            resource.StockExpirationDate,
            resource.AddedQuantity);
    }
}