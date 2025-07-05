using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.Rest.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

public class CreateAccountCommandFromResourceAssembler
{
    public static CreateAccountCommand ToCommand(CreateAccountResource resource)
    {
        if (resource is null)
            throw new ArgumentNullException(nameof(resource));

        return new CreateAccountCommand(
            resource.UserId,
            resource.Email.Trim(),
            resource.Role,
            resource.BusinessName,
            resource.StreetAddress
        );
    }
}