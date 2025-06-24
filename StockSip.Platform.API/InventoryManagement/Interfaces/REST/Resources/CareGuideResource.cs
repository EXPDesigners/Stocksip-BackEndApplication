namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the care guide resource.
/// </summary>
public record CareGuideResource(
    string Id,
    string AccountId,
    string? ProductId, 
    string Title, 
    string Summary, 
    double MinTemp, 
    double MaxTemp,
    string PlaceStorage, 
    string Recommendation);