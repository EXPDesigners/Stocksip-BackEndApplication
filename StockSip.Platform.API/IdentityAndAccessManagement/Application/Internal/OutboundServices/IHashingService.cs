namespace StockSip.Platform.API.IdentityAndAccessManagement.Application.Internal.OutboundServices;

/// <summary>
/// This interface is used to hash and verify passwords
/// </summary>
public interface IHashingService
{
    /// <summary>
    /// Hash a password
    /// </summary>
    /// <param name="password">
    /// The password to hash.
    /// </param>
    /// <returns>
    /// The hashed password.
    /// </returns>
    string HashPassword(string password);
    
    /// <summary>
    /// Verify a password against a hashed password
    /// </summary>
    /// <param name="password">
    /// The password to verify.
    /// </param>
    /// <param name="passwordHash">
    /// The hashed password to verify against.
    /// </param>
    /// <returns>
    /// True if the password matches the hash, otherwise false.
    /// </returns>
    bool VerifyPassword(string password, string passwordHash);
}