using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to sign up a new user.
/// </summary>
/// <param name="Username">
/// 
/// </param>
/// <param name="Password">
/// The password for the user, which is a required field. It is used for authentication.
/// </param>
public record SignUpCommand(string Username, string Password);