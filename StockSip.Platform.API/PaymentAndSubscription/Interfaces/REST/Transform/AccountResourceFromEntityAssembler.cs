using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

public class AccountResourceFromEntityAssembler
{
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