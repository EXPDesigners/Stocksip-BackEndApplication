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
    /// <param name="productId"> The identifier of the product that will be deleted. </param>
    /// <param name="warehouseId"> The identifier of the warehouse that won't store the product no more. </param>
    /// <returns>
    /// The resulting DeleteProductFromWarehouseCommand created from the resource.
    /// </returns>
    public static DeleteProductFromWarehouseCommand ToCommandFromResource(DeleteProductFromWarehouseResource resource, string productId, string warehouseId)
    {
        return new DeleteProductFromWarehouseCommand(productId, warehouseId, resource.ExpirationDate);
    }
}