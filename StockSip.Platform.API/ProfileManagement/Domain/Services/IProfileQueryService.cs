using StockSip.Platform.API.ProfileManagement.Domain.Model;
using StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;

namespace StockSip.Platform.API.ProfileManagement.Domain.Services;

/// <summary>
/// This interface defines the contract for profile query services.
/// </summary>
public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    Task<IEnumerable<Profile>> Handle(GetAllProfilesByAccountIdQuery query);
    
    Task<Profile?> Handle(GetProfileByIdQuery query);
    Task<Profile?> Handle(GetProfileByUserNameQuery query);
}