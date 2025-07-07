using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

public class AccountStatusFromEntityAssembler
{
    public AccountStatusResource ToResourceFromEntity(Account entity)
    {
        return new AccountStatusResource(entity.Status.ToString());
    }
}