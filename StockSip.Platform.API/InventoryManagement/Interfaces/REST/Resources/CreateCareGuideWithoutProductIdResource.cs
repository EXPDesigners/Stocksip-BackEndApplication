namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for the CreateCareGuideWithoutProductId command.
/// </summary>
public record CreateCareGuideWithoutProductIdResource(
    string Title, 
    string Summary, 
    double MinTemp, 
    double MaxTemp,
    string PlaceStorage, 
    string Recommendation);