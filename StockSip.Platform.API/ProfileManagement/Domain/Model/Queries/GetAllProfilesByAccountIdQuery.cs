using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.ProfileManagement.Domain.Model;

/// <summary>
/// This query is used to retrieve all profiles associated with a specific account.
/// </summary>
/// <param name="AccountId">
/// The unique identifier of the account for which profiles are to be retrieved.
/// </param>
public record GetAllProfilesByAccountIdQuery(AccountId AccountId);