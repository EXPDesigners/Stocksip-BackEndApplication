using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Authorization.Domain.Model.Queries;
using StockSip.Platform.API.Authorization.Domain.Repositories;
using StockSip.Platform.API.Authorization.Domain.Services;

namespace StockSip.Platform.API.Authorization.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService 
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.UserId);
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}