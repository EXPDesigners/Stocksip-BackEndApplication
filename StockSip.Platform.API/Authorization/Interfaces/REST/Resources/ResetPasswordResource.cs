namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for resetting a user's password.
/// </summary>
public record ResetPasswordResource(string Username, string NewPassword);