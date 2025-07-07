using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Services;

/// <summary>
/// This service is responsible for handling user commands such as sign-in and sign-up.
/// </summary>
public interface IUserCommandService
{
    
    /// <summary>
    /// This method handles the sign-in command for a user.
    /// </summary>
    Task<(User user, string token)> Handle(SignInCommand command);
    
    /// <summary>
    /// This method handles the sign-up command for a user.
    /// </summary>
    Task Handle(SignUpCommand command);
}