namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the resource for UpdateCareGuideCommand
/// </summary>
public record UpdateCareGuideResource(
    string NewTitle, 
    string NewSummary, 
    double NewMinTemp, 
    double NewMaxTemp,
    string NewPlaceStorage, 
    string NewRecommendation);