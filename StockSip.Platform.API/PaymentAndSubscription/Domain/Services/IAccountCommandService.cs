using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

/// <summary>
/// This interface defines the contract for account command services.
/// </summary>
public interface IAccountCommandService
{
    Task<Account?> Handle(CreateAccountCommand command);
}