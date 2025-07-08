using StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.ProfileManagement.Domain.Services;

/// <summary>
/// This service is responsible for handling commands related to profile management.
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    /// This method handles the creation of a new profile based on the provided command.
    /// </summary>
    Task<Profile?> Handle(CreateProfileCommand command);
}