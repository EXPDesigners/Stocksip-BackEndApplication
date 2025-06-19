using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a RegisterProductExitResource into a RegisterProductExitCommand.
/// </summary>
public static class RegisterProductExitCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms a RegisterProductExitResource into a RegisterProductExitCommand.
    /// </summary>
    /// <param name="resource">
    /// The RegisterProductExitResource to transform. It contains the details of the product exit.
    /// </param>
    /// <param name="warehouseId">
    /// The Warehouse ID where the product exit is registered. This is used to identify the warehouse context for the exit operation.
    /// </param>
    /// <param name="productId">
    /// The Product ID of the product that is exiting the warehouse. This is used to identify which product is being exited.
    /// </param>
    /// <returns>
    /// A RegisterProductExitCommand created from the resource, which encapsulates the details of the product exit operation.
    /// </returns>
    public static RegisterProductExitCommand ToCommandFromResource(
        RegisterProductExitResource resource,
        string warehouseId,
        string productId)
    {
        return new RegisterProductExitCommand(
            warehouseId,
            productId,
            resource.ExpirationDate,
            resource.QuantityExited,
            resource.ExitReason);
    }
}