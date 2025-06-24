namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to update the recommendations of the care guide.
/// </summary>
public record UpdateCareGuideCommand(
    string CareGuideId,
    string NewTitle, 
    string NewSummary, 
    double NewMinTemp, 
    double NewMaxTemp,
    string NewPlaceStorage, 
    string NewRecommendation);