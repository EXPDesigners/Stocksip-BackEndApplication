using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

public class CreateWarehouseCommandFromResourceAssembler
{
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