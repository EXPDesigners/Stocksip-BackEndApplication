using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command represents the creation of a new warehouse.
/// </summary>
public record CreateWarehouseCommand(string Name, 
                                     string Street,
                                     string City,
                                     string District,
                                     string PostalCode,
                                     string Country, 
                                     double MaxTemperature,
                                     double MinTemperature,
                                     double Capacity,
                                     string ProfileId,
                                     IFormFile? Image);