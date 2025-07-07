namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to sign in a user.
/// </summary>
/// <param name="Username">
/// The username of the user attempting to sign in.
/// </param>
/// <param name="Password">
/// The password of the user attempting to sign in.
/// </param>
public record SignInCommand(string Username, string Password);