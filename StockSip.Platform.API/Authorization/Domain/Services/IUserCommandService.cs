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
    /// Handle the sign in command to authenticate a user and return a token.
    /// </summary>
    Task<(User user, string token, string? accountId)> Handle(SignInCommand command);
    
    /// <summary>
    /// Handle the sign-up command to create a new user account.
    /// </summary>
    Task Handle(SignUpCommand command);
}