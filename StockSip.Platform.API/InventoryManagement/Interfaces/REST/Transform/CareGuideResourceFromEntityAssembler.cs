using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CareGuide entity into a CareGuideResource.
/// </summary>
public static class CareGuideResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a CareGuide entity into a CareGuideResource.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>
    /// A new CareGuideResource object.
    /// </returns>
    public static CareGuideResource ToResourceFromEntity(CareGuide entity)
    {
        return new CareGuideResource(
            entity.Id,
            entity.AccountId.ToString(),
            entity.ProductId,
            entity.Title,
            entity.Summary,
            entity.RecommendedMinTemperature,
            entity.RecommendedMaxTemperature,
            entity.RecommendedPlaceStorage,
            entity.GeneralRecommendation);
    }
}