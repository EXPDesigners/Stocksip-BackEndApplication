using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// This command is used to update an existing warehouse in the system.
/// <summary>
/// This record defines the properties required to update a warehouse, including its ID, name, address, temperature range, capacity, and an image URL.
/// </summary>
/// <param name="WarehouseId">The unique identifier of the warehouse</param>
/// <param name="Name">The name of the warehouse</param>
/// <param name="Address">The address of the warehouse</param>
/// <param name="Capacity">The capacity of the warehouse</param>
/// <param name="Temperature">The temperature range in the warehouse</param>
/// <param name="ImageUrl">The image url of the warehouse</param>
/// <param name="ProfileId">The unique identifier of the warehouse</param>
public record UpdateWarehouseCommand(long WarehouseId, string Name, WarehouseAddress Address, Capacity Capacity, Temperature Temperature, string ImageUrl, long ProfileId);