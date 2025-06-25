using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Aggregates;

public partial class User()
{
    /// <summary>
    /// The unique identifier for the user. It is generated as a new GUID hash code.
    /// </summary>
    public int Id { get; } = Guid.NewGuid().GetHashCode();
    
    private UserName Email { get; set; }

    /// <summary>
    /// The username of the user, which is a required field. It is used for authentication and must be unique.
    /// This field is completed with the email address of the user.
    /// </summary>
    public string Username => Email.GetUserName();

    /// <summary>
    /// The password for the user, which is a required field. It is used for authentication.
    /// </summary>
    [JsonIgnore] public string PasswordHash { get; private set; }
    
    /// <summary>
    /// This is the secondary email address for the user. It is an optional field that can be used for account recovery or notifications.
    /// </summary>
    public string SecondaryEmail { get; set; } = string.Empty;
    
    /// <summary>
    /// This is the role of the user within the system. It is a required field that determines the permissions and access level of the user.
    /// </summary>
    public ERoles Role { get; set; }
    
    /// <summary>
    /// This is the account ID associated with the user. It is a required field that links the user to a global account of the business.
    /// </summary>
    public AccountId AccountId { get; private set; }

    /// <summary>
    /// Constructor for the User class.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="passwordHash"></param>
    public User(string username, string passwordHash) : this()
    {
        Email = new UserName(username);
        PasswordHash = passwordHash;
    }
    
    /// <summary>
    /// Default constructor for the User class.
    /// </summary>
    /// <param name="accountId">
    /// The account ID associated with the user. It is a required field that links the user to a global account of the business.
    /// </param>
    /// <param name="username">
    /// The username of the user, which is a required field. It is used for authentication and must be unique.
    /// </param>
    /// <param name="passwordHash">
    /// The password for the user, which is a required field. It is used for authentication.
    /// </param>
    /// <param name="role">
    /// The role of the user within the system. It is a required field that determines the permissions and access level of the user.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the accountId is null.
    /// </exception>
    public User(string username, string passwordHash, ERoles role, AccountId accountId) : this()
    {
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash), "Password cannot be null.");
        Email = new UserName(username);
        AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId), "AccountId cannot be null.");
        Role = role;
    }
    
    
    /// <summary>
    /// This method changes the user's password to a new password.
    /// </summary>
    /// <param name="newPasswordHash">
    /// The new password to be set for the user. It must meet the specified criteria for a valid password.
    /// </param>
    /// <returns>
    /// The updated user instance with the new password hash.
    /// </returns>
    public User ChangePasswordHash(string newPasswordHash)
    {
        // Assign the new password to the Password property
        PasswordHash = newPasswordHash;
        
        // Return the updated user instance
        return this;
    }
    
    /// <summary>
    /// Updates the username of the user to a new username.
    /// </summary>
    /// <param name="newUsername">
    /// The new username to be set for the user. It should be a non-empty string that represents an email address.
    /// </param>
    /// <returns>
    /// The updated user instance with the new username.
    /// </returns>
    public User UpdateUsername(string newUsername)
    {
        // Validate the new username
        var userName = new UserName(newUsername);
        
        // Update the Email property with the new username
        Email = userName;
        
        // Return the updated user instance
        return this;
    }
}