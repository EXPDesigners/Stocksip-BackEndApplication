namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;

/// <summary>
/// This interface defines the contract for external authentication services.
/// </summary>
public interface IExternalAuthenticationService
{
    /// <summary>
    /// The CreateUserAsync method is used to create a new user account with the provided username and password.
    /// </summary>
    Task<string> CreateUserAsync(string username, string password);
}