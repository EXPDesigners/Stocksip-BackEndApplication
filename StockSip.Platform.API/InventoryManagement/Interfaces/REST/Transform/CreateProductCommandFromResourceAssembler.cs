using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CreateProductResource into a CreateProductCommand.
/// </summary>
public static class CreateProductCommandFromResourceAssembler
{
    /// <summary>
    /// This method transforms a CreateProductResource into a CreateProductCommand.
    /// </summary>
    /// <param name="resource"> The CreateProductResource to transform. </param>
    /// <returns> The CreateProductCommand created from the resource. </returns>
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
    {
        return new CreateProductCommand(resource.Name,
            resource.LiquorType,
            resource.BrandName,
            resource.UnitPriceAmount,
            resource.MinimumStock,
            resource.Image,
            resource.ProviderId);
    }
}