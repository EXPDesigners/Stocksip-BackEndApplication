using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

/// <summary>
/// This interface defines the contract for account command services.
/// </summary>
public interface IAccountCommandService
{
    /// <summary>
    /// This method handles the creation of an account based on the provided command.
    /// </summary>
    Task<Account?> Handle(CreateAccountCommand command);
}