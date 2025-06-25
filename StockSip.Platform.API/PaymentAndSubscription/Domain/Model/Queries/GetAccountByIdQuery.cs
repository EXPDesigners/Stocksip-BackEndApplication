namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve an account by its unique identifier.
/// </summary>
/// <param name="AccountId">The unique identifier of the account to be retrieved.</param>
public record GetAccountByIdQuery(string AccountId);