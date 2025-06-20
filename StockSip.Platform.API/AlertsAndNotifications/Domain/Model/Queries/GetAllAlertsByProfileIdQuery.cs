using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve all alerts associated with a specific profile.
/// </summary>
/// <param name="ProfileId">
/// The unique identifier of the profile for which alerts are being requested.
/// </param>
public record GetAllAlertsByProfileIdQuery(ProfileId ProfileId);