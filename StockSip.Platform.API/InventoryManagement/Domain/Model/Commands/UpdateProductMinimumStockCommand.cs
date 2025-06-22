namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to update only the minimum stock level of a specific product.
/// </summary>
/// <param name="ProductId"></param>
/// <param name="NewMinimumStock"></param>
public record UpdateProductMinimumStockCommand(string ProductId, int NewMinimumStock);