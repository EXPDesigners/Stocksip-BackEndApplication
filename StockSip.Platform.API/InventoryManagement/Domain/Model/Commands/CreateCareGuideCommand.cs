using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command used to create a new care guide assigning it to a product.
/// </summary>
/// <param name="AccountId">
/// The unique identifier for the owner of the care guide.
/// </param>
/// <param name="ProductId">
/// The unique identifier of the product care guide
/// </param>
/// <param name="Title">
/// The title of the document.
/// </param>
/// <param name="Summary">
/// The summary of all the recommendations.
/// </param>
/// <param name="MinTemp">
/// The recommended minimum temperature for a product.
/// </param>
/// <param name="MaxTemp">
/// The recommended maximum temperature for a product.
/// </param>
/// <param name="PlaceStorage">
/// The recommended place storage for the product.
/// </param>
/// <param name="Recommendation">
/// A general recommendation about the product.
/// </param>
public record CreateCareGuideCommand(
    AccountId AccountId,
    string ProductId, 
    string Title, 
    string Summary, 
    double MinTemp, 
    double MaxTemp,
    string PlaceStorage, 
    string Recommendation);