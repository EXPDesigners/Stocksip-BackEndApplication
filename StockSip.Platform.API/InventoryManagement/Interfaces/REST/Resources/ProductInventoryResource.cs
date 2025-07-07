using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the product resource.
/// </summary>
public record ProductInventoryResource(string ProductId,
                                       string Name,
                                       string Type,
                                       decimal UnitPriceAmount,
                                       int MinimumStock,
                                       string ImageUrl,
                                       int CurrentStock, 
                                       string Status, 
                                       DateOnly BestBeforeDate);