namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

public record WarehouseResource(string Name, 
                                string Address,
                                double MaxTemperature,
                                double MinTemperature,
                                double Capacity,
                                string ImageUrl);