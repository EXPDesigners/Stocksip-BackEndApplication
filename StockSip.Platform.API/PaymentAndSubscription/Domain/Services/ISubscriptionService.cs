using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

/// <summary>
/// This interface defines the contract for subscription services.
/// </summary>
///
public interface ISubscriptionService
{
    /// <summary>
    /// this method handles the creation of a subscription based on the provided command.
    /// </summary>
    Task<Subscription?> Handle(CreateSubscriptionCommand command);
}