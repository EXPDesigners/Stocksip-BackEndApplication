using StockSip.Platform.API.Authorization.Domain.Model.Commands;
using StockSip.Platform.API.Authorization.Domain.Model.Queries;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.Authorization.Interfaces.ACL;

namespace StockSip.Platform.API.Authorization.Application.Internal.ACL;

public class AuthenticationContextFacade(     
    IUserCommandService userCommandService,
    IUserQueryService userQueryService
    ) : IAuthenticationContextFacade
{
    public async Task<string> CreateUserAsync(string username, string password)
    {
        var createdUserCommand = new SignUpCommand(username, password);
        await userCommandService.Handle(createdUserCommand);
        
        var getUserByUsername = new GetUserByUsernameQuery(username);
        var user = await userQueryService.Handle(getUserByUsername);
        
        return user?.UserId ?? throw new Exception("User creation failed");
    }
}