using StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.ProfileManagement.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByEmailAsync(UserEmail email);
}