namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve a user by their username.
/// </summary>
/// <param name="Username">
/// The username of the user to be retrieved. This is a required field and must be unique.
/// </param>
public record GetUserByUsernameQuery(string Username);