using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

/// <summary>
/// This interface defines the contract for account query services.
/// </summary>
public interface IAccountQueryService
{
    /// <summary>
    /// This method retrieves an account based on the provided query.
    /// </summary>
    Task<Account?> Handle(GetAccountByIdQuery query);
    
    /// <summary>
    /// This method retrieves an account based on the provided email address query.
    /// </summary>
    Task<Account?> Handle(GetAccountByEmailQuery query);
    
    
    Task<string?> Handle(GetAccountStatusByIdQuery query);
}