namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

public record UpdateProductCommand(double UpdatedUnitPriceAmount, int UpdatedMinimumStock, string UpdatedImageUrl);