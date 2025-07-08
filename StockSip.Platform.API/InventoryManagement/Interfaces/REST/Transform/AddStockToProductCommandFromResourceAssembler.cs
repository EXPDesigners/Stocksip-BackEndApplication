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
    /// <param name="productId"> The identifier of the product that will receive the added stock. </param>
    /// <param name="warehouseId"> The identifier of the warehouse that stores the product whose stock will be updated. </param>
    /// <returns> The AddStockToProductCommand created from the resource. </returns>
    public static AddStockToProductCommand ToCommandFromResource(AddStockToProductResource resource, string productId, string warehouseId)
    {
        return new AddStockToProductCommand(
            productId, 
            warehouseId, 
            resource.StockExpirationDate,
            resource.AddedQuantity);
    }
}