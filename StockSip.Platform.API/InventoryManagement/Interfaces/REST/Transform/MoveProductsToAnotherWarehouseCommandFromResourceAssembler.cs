using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a MoveProductsToAnotherWarehouseResource into a MoveProductsToAnotherWarehouseCommand.
/// </summary>
public static class MoveProductsToAnotherWarehouseCommandFromResourceAssembler
{
    /// <summary>
    /// This method transforms a MoveProductsToAnotherWarehouseResource into a MoveProductsToAnotherWarehouseCommand.
    /// </summary>
    /// <param name="resource">
    /// The MoveProductsToAnotherWarehouseResource to be transformed into a command.
    /// </param>
    /// <returns>
    /// The MoveProductsToAnotherWarehouseCommand created from the resource.
    /// </returns>
    public static MoveProductsToAnotherWarehouseCommand ToCommandFromResource(
        MoveProductsToAnotherWarehouseResource resource)
    {
        return new MoveProductsToAnotherWarehouseCommand(resource.ProductId, 
            resource.OldWarehouseId,
            resource.NewWarehouseId, 
            resource.MovedStockExpirationDate, 
            resource.MovedQuantity);
    }
}