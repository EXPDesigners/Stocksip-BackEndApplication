using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a CompleteUpgradeResource into a CompleteUpgradeCommand.
/// </summary>
public class CompleteUpgradeFromResourceAssembler
{
    /// <summary>
    /// Transforms a CompleteUpgradeResource into a CompleteUpgradeCommand.
    /// </summary>
    public static CompleteUpgradeCommand ToCommandFromResource(CompleteUpgradeResource resource)
    {
        return new CompleteUpgradeCommand(
            resource.Token,
            resource.AccountId,
            resource.PlanId
        );
    }
}