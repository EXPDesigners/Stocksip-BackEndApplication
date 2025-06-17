using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

public class WarehouseResourceFromEntityAssembler
{
    public static WarehouseResource ToResourceFromEntity(Warehouse entity)
    {
        return new WarehouseResource(
            entity.Name,
            entity.Address.GetFullAddress(),
            entity.Temperature.MaxTemperature,
            entity.Temperature.MinTemperature,
            entity.Capacity.TotalCapacity,
            entity.ImageUrl.ToString()
        );
    }
}