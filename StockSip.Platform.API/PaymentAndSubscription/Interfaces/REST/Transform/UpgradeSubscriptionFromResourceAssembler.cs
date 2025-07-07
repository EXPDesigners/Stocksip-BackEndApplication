using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an UpgradeSubscriptionResource into an UpgradeSubscriptionCommand.
/// </summary>
public class UpgradeSubscriptionFromResourceAssembler
{
    /// <summary>
    /// Defines a method to transform an UpgradeSubscriptionResource into an UpgradeSubscriptionCommand.
    /// </summary>
    public static UpgradeSubscriptionCommand ToCommandFromResource(UpgradeSubscriptionResource resource)
    {
        return new UpgradeSubscriptionCommand(
            resource.AccountId,
            resource.PlanId
        );
    }
}