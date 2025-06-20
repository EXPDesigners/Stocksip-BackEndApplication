using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Alert aggregates.
/// </summary>
public interface IAlertRepository : IBaseRepository<Alert>
{
    /// <summary>
    /// This method retrieves all alerts associated with a specific product ID.
    /// </summary>
    Task<IEnumerable<Alert>> FindByProductIdAsync(ProductId productId);
    
    /// <summary>
    /// This method retrieves all alerts associated with a specific profile ID.
    /// </summary>
    Task<IEnumerable<Alert>> FindByProfileIdAsync(ProfileId profileId);
    
    /// <summary>
    /// This method retrieves all alerts associated with a specific warehouse ID.
    /// </summary>
    Task<IEnumerable<Alert>> FindByWarehouseIdAsync(WarehouseId warehouseId);
}