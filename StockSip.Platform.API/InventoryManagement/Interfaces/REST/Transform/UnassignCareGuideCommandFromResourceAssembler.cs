using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an UnassignCareGuideResource into an UnassignCareGuideCommand 
/// </summary>
public static class UnassignCareGuideCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms an UnassignCareGuideResource into an UnassignCareGuideCommand.
    /// </summary>
    /// <returns>
    /// The created UnassignCareGuideCommand.
    /// </returns>
    public static UnassignCareGuideCommand ToCommandFromResource(string careGuideId)
    {
        return new UnassignCareGuideCommand(careGuideId);
    }
}