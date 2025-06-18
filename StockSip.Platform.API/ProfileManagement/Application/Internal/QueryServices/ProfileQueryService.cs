using StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.ProfileManagement.Domain.Model.Queries;
using StockSip.Platform.API.ProfileManagement.Domain.Repositories;
using StockSip.Platform.API.ProfileManagement.Domain.Services;

namespace StockSip.Platform.API.ProfileManagement.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileReository
    }
}
