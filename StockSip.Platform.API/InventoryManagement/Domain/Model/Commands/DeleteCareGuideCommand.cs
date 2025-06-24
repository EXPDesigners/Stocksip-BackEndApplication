namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to delete a care guide.
/// </summary>
/// <param name="CareGuideId">
/// The unique identifier of the care guide that will be deleted.
/// </param>
public record DeleteCareGuideCommand(string CareGuideId);