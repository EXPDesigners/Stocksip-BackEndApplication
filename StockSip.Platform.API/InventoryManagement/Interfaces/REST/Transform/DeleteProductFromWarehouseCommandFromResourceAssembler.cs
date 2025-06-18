using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a DeleteProductFromWarehouseResource into a DeleteProductFromWarehouseCommand.
/// </summary>
public static class DeleteProductFromWarehouseCommandFromResourceAssembler
{
    /// <summary>
    /// Method to transform a DeleteProductFromWarehouseResource into a DeleteProductFromWarehouseCommand.
    /// </summary>
    /// <param name="resource">
    /// The DeleteProductFromWarehouseResource to be transformed.
    /// </param>
    /// <returns>
    /// The resulting DeleteProductFromWarehouseCommand created from the resource.
    /// </returns>
    public static DeleteProductFromWarehouseCommand ToCommandFromResource(DeleteProductFromWarehouseResource resource)
    {
        return new DeleteProductFromWarehouseCommand(resource.ProductId, resource.WarehouseId, resource.ExpirationDate);
    }
}