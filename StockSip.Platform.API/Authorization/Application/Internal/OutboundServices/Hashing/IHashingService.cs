namespace StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Hashing;

/// <summary>
/// The hashing service interface for password management.
/// </summary>
public interface IHashingService
{
    /// <summary>
    /// Hashes a password using a secure hashing algorithm.
    /// </summary>
    string HashPassword(string password);
    
    /// <summary>
    /// Verifies a password against a hashed password.
    /// </summary>
    bool VerifyPassword(string password, string passwordHash);
}