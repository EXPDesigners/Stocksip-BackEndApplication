namespace StockSip.Platform.API.Authorization.Domain.Model.Queries;

/// <summary>
/// The query to get a user by username.
/// </summary>
public record GetUserByUsernameQuery(string Username);