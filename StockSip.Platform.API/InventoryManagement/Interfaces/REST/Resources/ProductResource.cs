using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the product resource.
/// </summary>
public record ProductResource(string ImageUrl, 
                                string? AdditionalName, 
                                string BrandName, 
                                string LiquorType, 
                                int UnitPriceAmount,
                                int MinimumStock,
                                int CurrentStock,
                                DateTime ExpirationDate,
                                WarehouseResource Warehouse,
                                string? ProviderId = null);