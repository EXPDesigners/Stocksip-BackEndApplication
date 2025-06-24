using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an AssignCareGuideToProductResource into an AssignCareGuideToProductCommand.
/// </summary>
public static class AssignCareGuideToProductCommandFromResource
{
    /// <summary>
    /// Transforms an AssignCareGuideToProductResource into an AssignCareGuideToProductCommand.
    /// </summary>
    /// <returns>
    /// The transformed AssignCareGuideToProductCommand.
    /// </returns>
    public static AssignCareGuideToProductCommand ToCommandFromResource(string careGuideId, string productId)
    {
        return new AssignCareGuideToProductCommand(
            careGuideId,
            productId);
    }
}