using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an AddProductsToWarehouseResource into an AddProductsToWarehouseCommand.
/// </summary>
public static class AddProductsToWarehouseCommandFromResourceAssembler
{
    /// <summary>
    /// Method to transform an AddProductsToWarehouseResource into an AddProductsToWarehouseCommand.
    /// </summary>
    /// <param name="resource"> The AddProductsToWarehouseResource to transform. </param>
    /// <returns> The AddProductsToWarehouseCommand created from the resource. </returns>
    public static AddProductsToWarehouseCommand ToCommandFromResource(AddProductsToWarehouseResource resource)
    {
        return new AddProductsToWarehouseCommand(resource.ProductId, resource.WarehouseId, resource.ExpirationDate, resource.Quantity);
    }
}