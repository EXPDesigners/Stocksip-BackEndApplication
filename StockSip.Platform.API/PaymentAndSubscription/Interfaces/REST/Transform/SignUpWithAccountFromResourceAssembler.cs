using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a SignUpWithAccountResource into a SignUpWithAccountCommand.
/// </summary>
public class SignUpWithAccountFromResourceAssembler
{
    /// <summary>
    /// Defines a method to convert a SignUpWithAccountResource to a SignUpWithAccountCommand.
    /// </summary>
    public static SignUpWithAccountCommand ToCommandFromResource(SignUpWithAccountResource resource)
    {
        return new SignUpWithAccountCommand(
            resource.Username,
            resource.Password,
            resource.ValidatePassword,
            resource.AccountRole,
            resource.BusinessName
        );
    }
}