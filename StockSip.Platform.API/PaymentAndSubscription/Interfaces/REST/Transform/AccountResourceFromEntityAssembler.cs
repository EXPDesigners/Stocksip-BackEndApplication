using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an Account entity into an AccountResource.
/// </summary>
public class AccountResourceFromEntityAssembler
{
    /// <summary>
    /// Defines a method to convert an Account entity to an AccountResource.
    /// </summary>
    public static AccountResource ToResourceFromEntity(Account entity)
    {
        return new AccountResource(
            entity.AccountId,
            entity.BusinessName.Name,
            entity.Status.ToString(),
            entity.AccountRole.ToString(),
            entity.GetCreationDate()
        );
    }
}