using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;

namespace StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Token;

/// <summary>
/// Defines the contract for a token service that handles token generation and validation.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a token for the given user.
    /// </summary>
    string GenerateToken(User user);
    
    /// <summary>
    /// Validates the provided token and returns the user ID if valid.
    /// </summary>
    Task<string?> ValidateToken(string token);
}