using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the product resource.
/// </summary>
public record ProductResource(string ProductId,
                                string ImageUrl, 
                                string Name, 
                                string BrandName, 
                                string LiquorType, 
                                double UnitPriceAmount,
                                int MinimumStock,
                                string ProviderId);