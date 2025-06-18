using StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;
using StockSip.Platform.API.ProfileManagement.Domain.Model.Queries;
using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.ProfileManagement.Domain.Services;
using StockSip.Platform.API.ProfileManagement.Interfaces.ACL;

namespace StockSip.Platform.API.ProfileManagement.Application.ACL;

public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService) 
    : IProfilesContextFacade
{
    public async Task<string> CreateProfile(string name, string email, string businessAddress, string businessName, string phoneNumber, string role)
    {
        var createProfileCommand = new CreateProfileCommand(name, email, phoneNumber, role, businessName, businessAddress);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id;
    }
    
    public async Task<string> FetchProfileByEmail(string email)
    {
        var getProfileByEmailQuery = new GetProfileByEmailQuery();
        var profile = await profileQueryService.GetProfileByEmailQuery(new UserEmail(email));
        return profile?.Id;
    }
}