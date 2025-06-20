using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.AlertsAndNotifications.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// This class implements the IAlertRepository interface, providing methods to interact with the Alert aggregate.
/// </summary>
public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    /// <summary>
    /// This async method retrieves all alerts associated with a specific product ID.
    /// </summary>
    /// <param name="productId">
    /// The ID of the product whose alerts are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of alerts that belong to the specified product ID.
    /// </returns>
    public async Task<IEnumerable<Alert>> FindByProductIdAsync(ProductId productId)
    {
        return await Context.Set<Alert>()
            .Where(alert => alert.ProductId == productId)
            .ToListAsync();
    }

    /// <summary>
    /// This async method retrieves all alerts associated with a specific profile ID.
    /// </summary>
    /// <param name="profileId">
    /// The ID of the profile whose alerts are to be retrieved.
    /// </param>
    /// <returns>
    /// The list of alerts that belong to the specified profile ID.
    /// </returns>
    public async Task<IEnumerable<Alert>> FindByProfileIdAsync(ProfileId profileId)
    {
        return await Context.Set<Alert>()
            .Where(alert => alert.ProfileId == profileId)
            .ToListAsync();
    }

    /// <summary>
    /// This async method retrieves all alerts associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    /// The ID of the warehouse whose alerts are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of alerts that belong to the specified warehouse ID.
    /// </returns>
    public async Task<IEnumerable<Alert>> FindByWarehouseIdAsync(WarehouseId warehouseId)
    {
        return await Context.Set<Alert>()
            .Where(alert => alert.WarehouseId == warehouseId)
            .ToListAsync();
    }
}