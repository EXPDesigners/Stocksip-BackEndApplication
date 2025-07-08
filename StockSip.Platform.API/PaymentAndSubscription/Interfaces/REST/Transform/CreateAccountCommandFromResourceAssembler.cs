using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.Rest.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

public static class CreateAccountCommandFromResourceAssembler
{
    public static CreateAccountCommand ToCommand(CreateAccountResource r) => new(
        OwnerUserId:  r.OwnerUserId,
        Email:        r.Email,
        BusinessName: r.BusinessName,
        AccountRole:  r.AccountRole
    );
}