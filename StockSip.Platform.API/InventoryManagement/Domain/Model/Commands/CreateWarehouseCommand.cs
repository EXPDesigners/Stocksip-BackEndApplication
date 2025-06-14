using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// This command is used to create a new warehouse in the system.
/// <summary>
/// this record defines the properties required to create a new warehouse, including its name, address, temperature range, capacity, and an image URL.
/// </summary>
/// <param name="Name">The name of the warehouse</param>
/// <param name="Address">The address of the warehouse</param>
/// <param name="Temperature">The temperature range in the warehouse</param>
/// <param name="Capacity">The capacity of the warehouse</param>
/// <param name="imageUrl">the image url of the warehouse</param>
public record CreateWarehouseCommand(String Name, WarehouseAddress Address, Temperature Temperature, Capacity Capacity, string imageUrl);