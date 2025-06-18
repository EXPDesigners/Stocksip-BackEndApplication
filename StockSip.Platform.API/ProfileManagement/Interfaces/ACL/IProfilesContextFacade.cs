namespace StockSip.Platform.API.ProfileManagement.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<int> CreateProfile(
        string name,
        string email,
        string businessAddress,
        string businessName,
        string phoneNumber,
        string role
    );
    
    Task<int> FetchProfileByEmail(
        string email
    );

}