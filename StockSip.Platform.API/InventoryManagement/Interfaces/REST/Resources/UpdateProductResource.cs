namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record is used to update the details of a product in the warehouse.
/// </summary>
public record UpdateProductResource(string ProductId, double UpdatedUnitPriceAmount, int UpdatedMinimumStock, string UpdatedImageUrl);