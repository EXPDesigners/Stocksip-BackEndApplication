namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for the CreateCareGuide command.
/// </summary>
public record CreateCareGuideResource( 
    string Title, 
    string Summary, 
    double MinTemp, 
    double MaxTemp,
    string PlaceStorage, 
    string Recommendation);