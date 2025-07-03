using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace StockSip.Platform.API.Authorization.Infrastructure.Hashing.BCrypt.Services;

/// <summary>
/// This class provides methods for hashing and verifying passwords using BCrypt.
/// </summary>
public class HashingService : IHashingService
{
    
    /// <summary>
    /// This method hashes a password using BCrypt.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>A hashed version of the password.</returns>
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    /// <summary>
    /// This method verifies a password against a hashed password using BCrypt.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="passwordHash">The hashed password to verify against.</param>
    /// <returns>True if the password matches the hash, otherwise false.</returns>
    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}