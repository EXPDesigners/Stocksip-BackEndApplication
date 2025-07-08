using StockSip.Platform.API.Authorization.Interfaces.ACL;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.ACL;

/// <summary>
/// Defines the service for external authentication operations.
/// </summary>
/// <param name="authenticationContext">The authentication context facade for user operations.</param>
public class ExternalAuthenticationService(IAuthenticationContextFacade authenticationContext) : IExternalAuthenticationService
{
    /// <summary>
    /// Defines the method to create a user asynchronously.
    /// </summary>
    public async Task<string> CreateUserAsync(string username, string password)
    {
        return await authenticationContext.CreateUserAsync(username, password);
    }
}