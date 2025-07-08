using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an UpdateCareGuideResource into an UpdateCareGuideCommand.
/// </summary>
public static class UpdateCareGuideCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms an UpdateCareGuideResource into an UpdateCareGuideCommand.
    /// </summary>
    /// <returns>
    /// The created UpdateCareGuideCommand command.
    /// </returns>
    public static UpdateCareGuideCommand ToCommandFromResource(UpdateCareGuideResource resource, string careGuideId)
    {
        return new UpdateCareGuideCommand(
            careGuideId,
            resource.NewTitle,
            resource.NewSummary,
            resource.NewMinTemp,
            resource.NewMaxTemp,
            resource.NewPlaceStorage,
            resource.NewRecommendation);
    }
}