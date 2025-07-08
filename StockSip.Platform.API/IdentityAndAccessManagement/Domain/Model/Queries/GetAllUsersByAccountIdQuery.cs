using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve all users associated with a specific account.
/// </summary>
/// <param name="AccountId">
/// The unique identifier of the account for which users are being retrieved.
/// </param>
public record GetAllUsersByAccountIdQuery(AccountId AccountId);