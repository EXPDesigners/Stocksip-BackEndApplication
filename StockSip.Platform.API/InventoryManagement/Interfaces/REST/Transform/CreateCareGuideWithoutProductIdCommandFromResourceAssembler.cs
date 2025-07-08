using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CreateCareGuideWithoutProductIdResource into a CreateCareGuideWithoutProductIdCommand 
/// </summary>
public static class CreateCareGuideWithoutProductIdCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms a CreateCareGuideWithoutProductIdResource into a CreateCareGuideWithoutProductIdCommand.
    /// </summary>
    /// <returns>
    /// The created CreateCareGuideWithoutProductIdCommand.
    /// </returns>
    public static CreateCareGuideWithoutProductIdCommand ToCommandFromResource(
        CreateCareGuideWithoutProductIdResource resource, string accountId)
    {
        var targetAccountId = new AccountId(accountId);
        return new CreateCareGuideWithoutProductIdCommand(
            targetAccountId,
            resource.Title,
            resource.Summary,
            resource.MinTemp,
            resource.MaxTemp,
            resource.PlaceStorage,
            resource.Recommendation);
    }
}