namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents the resource for updating a warehouse.
/// </summary>
public record UpdateWarehouseResource(string Name,
                                      string Street,
                                      string City,
                                      string District,
                                      string PostalCode,
                                      string Country,
                                      double MinTemperature,
                                      double MaxTemperature,
                                      double Capacity,
                                      int ProfileId,
                                      string ImageUrl
                                      );