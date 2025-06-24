using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to create a new user profile in the system.
/// </summary>
/// <param name="FirstName">
/// The first name of the user.
/// </param>
/// <param name="LastName">
/// The last name of the user.
/// </param>
/// <param name="ContactNumber">
/// The contact number of the user.
/// </param>
/// <param name="UserId">
/// The unique identifier for the user associated with this profile.
/// </param>
public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string ContactNumber,
    UserId UserId);