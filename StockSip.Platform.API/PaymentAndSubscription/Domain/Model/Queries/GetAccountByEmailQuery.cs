namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;

/// <summary>
/// Query to retrieve an Account by its email address.
/// </summary>
public record GetAccountByEmailQuery(string Email);