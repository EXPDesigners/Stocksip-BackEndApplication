namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

public record UpdateProductCommand(string ProductId, double UpdatedUnitPriceAmount, int UpdatedMinimumStock, string UpdatedImageUrl);