using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CreateCareGuideCommandResource to a CreateCareGuideCommand.
/// </summary>
public static class CreateCareGuideCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms a CreateCareGuideCommandResource to a CreateCareGuideCommand.
    /// </summary>
    /// <returns>
    /// The created CreateCareGuideCommand.
    /// </returns>
    public static CreateCareGuideCommand ToCommandFromResource(CreateCareGuideResource resource, string accountId, string productId)
    {
        var targetAccountId = new AccountId(accountId);
            
        return new CreateCareGuideCommand(
            targetAccountId,
            productId,
            resource.Title,
            resource.Summary,
            resource.MinTemp,
            resource.MaxTemp,
            resource.PlaceStorage,
            resource.Recommendation);
    }
}