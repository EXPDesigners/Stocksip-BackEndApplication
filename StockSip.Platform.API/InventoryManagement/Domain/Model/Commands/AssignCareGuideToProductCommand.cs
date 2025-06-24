namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to assign a care guide to a product or to another product.
/// </summary>
/// <param name="CareGuideId">
/// The unique identifier of the care guide that will be assigned to a product.
/// </param>
/// <param name="ProductId">
/// The unique identifier of the product that will receive a care guide.
/// </param>
public record AssignCareGuideToProductCommand(string CareGuideId, string ProductId);