namespace StockSip.Platform.API.Authorization.Domain.Model.Queries;

/// <summary>
/// The query to get a user by user ID.
/// </summary>
public record GetUserByIdQuery(string UserId);