namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the warehouse resource.
/// </summary>
public record CreateWarehouseResource(string Name,
                                      string Street,
                                      string City,
                                      string District,
                                      string PostalCode,
                                      string Country,
                                      double MaxTemperature,
                                      double MinTemperature,
                                      double Capacity,
                                      int ProfileId
                                      );