using StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.ProfileManagement.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
    Task<Profile?> Handle(UpdateProfileCommand command);
}