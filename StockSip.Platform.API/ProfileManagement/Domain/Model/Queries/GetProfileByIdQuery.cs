namespace StockSip.Platform.API.ProfileManagement.Domain.Model;

/// <summary>
/// This query is used to retrieve a profile by its unique identifier.
/// </summary>
/// <param name="ProfileId">
/// The unique identifier of the profile to be retrieved.
/// </param>
public record GetProfileByIdQuery(string ProfileId);