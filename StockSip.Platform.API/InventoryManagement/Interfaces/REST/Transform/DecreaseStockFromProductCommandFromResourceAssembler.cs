using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a DecreaseStockFromProductResource into a DecreaseStockFromProductCommand.
/// </summary>
public static class DecreaseStockFromProductCommandFromResourceAssembler
{
    /// <summary>
    /// Method to transform a DecreaseStockFromProductResource into a DecreaseStockFromProductCommand.
    /// </summary>
    /// <param name="resource"> The DecreaseStockFromProductResource to transform. </param>
    /// <returns> The DecreaseStockFromProductCommand created from the resource. </returns>
    public static DecreaseStockFromProductCommand ToCommandFromResource(DecreaseStockFromProductResource resource)
    {
        return new DecreaseStockFromProductCommand(resource.ProductId, 
            resource.WarehouseId, 
            resource.ExpirationDate,
            resource.RemovedQuantity);
    }
}