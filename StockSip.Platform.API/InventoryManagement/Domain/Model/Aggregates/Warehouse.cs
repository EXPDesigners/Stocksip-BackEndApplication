using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

/// This aggregate root represents a warehouse in the inventory management system.
/// <summary>
/// This class defines the properties and behaviors of a warehouse, including its name, address, temperature range, capacity, and associated profile ID.
/// </summary>
public class Warehouse
{
    public long WarehouseId { get; }
    public string Name { get; private set; }
    public WarehouseAddress Address { get; internal set; }
    public Temperature Temperature { get; internal set; }
    public Capacity Capacity { get; internal set; }
    
    public string imageUrl { get; private set;  }
    
    public ProfileId Id { get; internal set; }

    public Warehouse(CreateWarehouseCommand command)
    {
        this.Name = command.Name;
        this.Address = command.Address;
        this.Temperature = command.Temperature;
        this.Capacity = command.Capacity;
    }
}