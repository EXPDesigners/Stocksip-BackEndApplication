using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Care Guide aggregates.
/// </summary>
public interface ICareGuideRepository : IBaseRepository<CareGuide>
{
    /// <summary>
    /// This method is used to find a care guide by its product id.
    /// </summary>
    Task<CareGuide?> FindByProductIdAsync(string productId);
    
    /// <summary>
    /// This method is used to find all the care guides related to a specific account id.
    /// </summary>
    Task<IEnumerable<CareGuide>> FindAllByAccountIdAsync(AccountId accountId);
}