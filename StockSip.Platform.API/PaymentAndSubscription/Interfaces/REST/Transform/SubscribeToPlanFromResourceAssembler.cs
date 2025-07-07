using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a SubscribeToPlanResource into a SubscribeToPlanCommand.
/// </summary>
public class SubscribeToPlanFromResourceAssembler
{
    /// <summary>
    /// A static method that converts a SubscribeToPlanResource into a SubscribeToPlanCommand.
    /// </summary>
    /// <param name="resource">A SubscribeToPlanResource object containing the necessary data.</param>
    /// <returns>A SubscribeToPlanCommand object initialized with the data from the resource.</returns>
    public static SubscribeToPlanCommand ToCommandFromResource(SubscribeToPlanResource resource)
    {
        return new SubscribeToPlanCommand(
            resource.AccountId,
            resource.PlanId
        );
    }
}