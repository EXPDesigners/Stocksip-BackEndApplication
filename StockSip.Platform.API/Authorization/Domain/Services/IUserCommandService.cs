using System.Reflection.Metadata;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Authorization.Domain.Model.Commands;

namespace StockSip.Platform.API.Authorization.Domain.Services;

/// <summary>
/// This interface defines the contract for user command services.
/// </summary>
public interface IUserCommandService
{
    /// <summary>
    /// Handles a sign-in command by validating the user credentials, generating an authentication token,
    /// and retrieving additional account details.
    /// </summary>
    /// <param name="command">The sign-in command containing the username and password.</param>
    /// <returns>A tuple containing the authenticated user, a JWT token, the account ID if available, and the account role if available.</returns>
    Task<(User user, string token, string? accountId, string? accountRole)> Handle(SignInCommand command);

    /// <summary>
    /// Handles a sign-up command by creating a new user, hashing the user's password,
    /// and validating the uniqueness of the username.
    /// </summary>
    /// <param name="command">The sign-up command containing the username and password.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Handle(SignUpCommand command);
}