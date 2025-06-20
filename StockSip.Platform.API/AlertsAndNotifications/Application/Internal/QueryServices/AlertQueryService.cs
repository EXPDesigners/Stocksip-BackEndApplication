using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Repositories;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Services;

namespace StockSip.Platform.API.AlertsAndNotifications.Application.Internal.QueryServices;

/// <summary>
/// This class implements the IAlertQueryService interface to handle queries related to alerts.
/// </summary>
/// <param name="alertRepository">
/// The repository for accessing alert data.
/// </param>
public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    /// <summary>
    /// This method retrieves an alert by its ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the alert ID.
    /// </param>
    /// <returns>
    /// The alert with the specified ID, or null if not found.
    /// </returns>
    public async Task<Alert?> Handle(GetAlertByIdQuery query)
    {
        return await alertRepository.FindByIdAsync(query.AlertId);
    }

    /// <summary>
    /// This async method retrieves all alerts for a specific product ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the product ID.
    /// </param>
    /// <returns>
    /// A list of alerts associated with the specified product ID.
    /// </returns>
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsByProductIdQuery query)
    {
        return await alertRepository.FindByProductIdAsync(query.ProductId);
    }

    /// <summary>
    /// This async method retrieves all alerts for a specific profile ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the profile ID.
    /// </param>
    /// <returns>
    /// A list of alerts associated with the specified profile ID.
    /// </returns>
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsByProfileIdQuery query)
    {
        return await alertRepository.FindByProfileIdAsync(query.ProfileId);
    }

    /// <summary>
    /// This async method retrieves all alerts for a specific warehouse ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the warehouse ID.
    /// </param>
    /// <returns>
    /// A list of alerts associated with the specified warehouse ID.
    /// </returns>
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsByWarehouseIdQuery query)
    {
        return await alertRepository.FindByWarehouseIdAsync(query.WarehouseId);
    }
}