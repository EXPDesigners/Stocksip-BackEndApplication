using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Aggregates;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Application.Internal.OutboundServices;

/// <summary>
/// This interface is used to generate and validate JWT tokens for user authentication.
/// </summary>
public interface ITokenService
{
    
    /// <summary>
    /// Generates a JWT token for the given user.
    /// </summary>
    /// <param name="user">
    /// The user for whom the token is generated.
    /// </param>
    /// <returns>
    /// The generated JWT token as a string.
    /// </returns>
    string GenerateToken(User user);

    /// <summary>
    /// Validates a JWT token and returns the user ID if valid.
    /// </summary>
    /// <param name="token">
    /// The JWT token to validate.
    /// </param>
    /// <returns>
    /// The user ID if the token is valid, otherwise null.
    /// </returns>
    Task<int?> ValidateToken(string token);
}