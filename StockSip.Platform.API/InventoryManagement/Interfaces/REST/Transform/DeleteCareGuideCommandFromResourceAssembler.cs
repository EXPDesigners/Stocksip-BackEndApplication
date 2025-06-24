using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a DeleteCareGuideResource into a DeleteCareGuideCommand. 
/// </summary>
public static class DeleteCareGuideCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms 
    /// </summary>
    /// <param name="careGuideId"></param>
    /// <returns></returns>
    public static DeleteCareGuideCommand ToCommandFromAssembler(string careGuideId)
    {
        return new DeleteCareGuideCommand(careGuideId);
    }
}