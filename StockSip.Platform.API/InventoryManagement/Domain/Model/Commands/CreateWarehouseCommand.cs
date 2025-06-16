using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;


public record CreateWarehouseCommand(string Name, 
                                     string Street,
                                     string City,
                                     string District,
                                     string PostalCode,
                                     string Country, 
                                     double MinTemperature,
                                     double MaxTemperature,
                                     double Capacity,
                                     string ImageUrl,
                                     int ProfileId);