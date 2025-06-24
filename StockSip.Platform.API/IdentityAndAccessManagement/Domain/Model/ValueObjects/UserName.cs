using System.Text.RegularExpressions;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the username of a user.
/// </summary>
public record UserName()
{
    private string Username;
    
    /// <summary>
    /// This constructor initializes a new instance of the <see cref="UserName"/> class with an empty username.
    /// </summary>
    /// <param name="username">
    /// The username to be set. It should be a non-empty string that represents an email address.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided username is null or empty, or if it does not match the email format.
    /// </exception>
    public UserName(string username) : this()
    {
        // Checks if the provided username is null or empty.
        if (IsEmailValid(username))
        {
            throw new ArgumentException("Username must be a valid email address.", nameof(username));
        }
        
        // Assigns the trimmed username to the Username property.
        Username = username.Trim();
    }

    /// <summary>
    /// This method validates the username to ensure it is a valid email format.
    /// </summary>
    /// <param name="username">
    /// The username to validate. It should be a non-empty string that represents an email address.
    /// </param>
    /// <returns>
    /// True if the username is a valid email format; otherwise, false.
    /// </returns>
    private static bool IsEmailValid(string username)
    {
        // Builds a regular expression pattern to validate the email format.
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        // Uses Regex.IsMatch to check if the username matches the email pattern.
        return Regex.IsMatch(username, pattern, RegexOptions.IgnoreCase);
    }
    
    /// <summary>
    /// This method returns the username as a trimmed string.
    /// </summary>
    /// <returns>
    /// The trimmed username string.
    /// </returns>
    public string GetUserName() => Username.Trim();
}