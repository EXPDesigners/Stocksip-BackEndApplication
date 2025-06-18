using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CreateWarehouseResource into a CreateWarehouseCommand.
/// </summary>
public class CreateWarehouseCommandFromResourceAssembler
{
    /// <summary>
    /// This method transforms a CreateWarehouseResource into a CreateWarehouseCommand.
    /// </summary>
    /// <param name="resource">The CreateWarehouseResource to transform.</param>
    /// <returns>The CreateWarehouseCommand created from the resource.</returns>
    public static CreateWarehouseCommand ToCommandFromResource(CreateWarehouseResource resource)
    {
        return new CreateWarehouseCommand(
            resource.Name,
            resource.Street,
            resource.City,
            resource.District,
            resource.PostalCode,
            resource.Country,
            resource.MaxTemperature,
            resource.MinTemperature,
            resource.Capacity,
            resource.ProfileId
        );
    }
}