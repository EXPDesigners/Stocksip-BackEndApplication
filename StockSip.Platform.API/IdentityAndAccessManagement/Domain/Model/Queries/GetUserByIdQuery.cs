namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve a specific user by their unique identifier.
/// </summary>
/// <param name="UserId">
/// The unique identifier of the user to be retrieved.
/// </param>
public record GetUserByIdQuery(string UserId);