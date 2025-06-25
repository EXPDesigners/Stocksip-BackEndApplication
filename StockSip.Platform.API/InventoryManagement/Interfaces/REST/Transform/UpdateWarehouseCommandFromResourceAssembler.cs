using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an UpdateWarehouseResource into an UpdateWarehouseCommand.
/// </summary>
public static class UpdateWarehouseCommandFromResourceAssembler
{
    /// <summary>
    /// This method transforms an UpdateWarehouseResource into an UpdateWarehouseCommand.
    /// </summary>
    /// <param name="resource">The UpdateWarehouseResource to transform.</param>
    /// <returns>A new UpdateWarehouseCommand created from the resource.</returns>
    public static UpdateWarehouseCommand ToCommandFromResource(UpdateWarehouseResource resource, string warehouseId)
    {
        return new UpdateWarehouseCommand(
            warehouseId,
            resource.Name,
            resource.Street,
            resource.City,
            resource.District,
            resource.PostalCode,
            resource.Country,
            resource.MaxTemperature,
            resource.MinTemperature,
            resource.Capacity
        );
    }
}