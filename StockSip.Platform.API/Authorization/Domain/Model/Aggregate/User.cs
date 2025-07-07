using System.Text.Json.Serialization;

namespace StockSip.Platform.API.Authorization.Domain.Model.Aggregate;

/// <summary>
/// The user aggregate root.
/// </summary>
/// <remarks>This class is used to represent user</remarks>
public class User(string username, string passwordHash)
{
    public User() : this(string.Empty, string.Empty)
    {
    }

    public string UserId { get; } = Guid.NewGuid().ToString();
    public string Username { get; private set; } = username;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public string? RecoveryCode { get; private set; }
    public DateTime? RecoveryCodeExpiration { get; private set; }

    /// <summary>
    /// Update the username of the user.
    /// </summary>
    /// <param name="username">The new username</param>
    /// <returns>The updated user</returns>
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /// <summary>
    /// Update the password hash of the user.
    /// </summary>
    /// <param name="passwordHash">The new password hash</param>
    /// <returns>The updated user</returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
    
    public void SetRecoveryCode(string recoveryCode, TimeSpan duration)
    {
        RecoveryCode = recoveryCode;
        RecoveryCodeExpiration = DateTime.UtcNow.Add(duration);
    }
    
    public bool IsRecoveryCodeValid(string inputCode)
    {
        return RecoveryCode == inputCode && RecoveryCodeExpiration > DateTime.UtcNow;
    }

    public void ClearRecoveryCode()
    {
        RecoveryCode = null;
        RecoveryCodeExpiration = null;
    }
}