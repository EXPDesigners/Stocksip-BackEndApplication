namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the warehouse resource.
/// </summary>
public record WarehouseResource(string Name, 
                                string Address,
                                double MaxTemperature,
                                double MinTemperature,
                                double Capacity,
                                string ImageUrl);