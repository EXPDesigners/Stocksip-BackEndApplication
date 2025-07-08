using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CompleteSubscriptionResource into a CompleteSubscriptionCommand.
/// </summary>
public class CompleteSubscriptionFromResourceAssembler
{
    /// <summary>
    /// This method transforms a CompleteSubscriptionResource into a CompleteSubscriptionCommand.
    /// </summary>
    /// <param name="resource">A CompleteSubscriptionResource object containing the necessary data.</param>
    /// <returns>A CompleteSubscriptionCommand object populated with the data from the resource.</returns>
    public static CompleteSubscriptionCommand ToCommandFromResource(CompleteSubscriptionResource resource)
    {
        return new CompleteSubscriptionCommand(
            resource.Token,
            resource.AccountId,
            resource.PlanId
        );
    }
}