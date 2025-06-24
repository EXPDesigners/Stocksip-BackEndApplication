namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// Command to unassign the care guide of the current product.
/// </summary>
/// <param name="CareGuideId">
/// The unique identifier of the care guide that will be unassigned from the current product.
/// </param>
public record UnassignCareGuideCommand(string CareGuideId);