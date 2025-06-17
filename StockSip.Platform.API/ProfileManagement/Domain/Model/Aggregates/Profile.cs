using StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;
using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;

public class Profile
{
    public string Id { get; }
    public UserName Name { get; private set; }
    public UserEmail Email { get; private set; }
    public UserBusinessAddress Address { get; private set; }
    public UserBusinessName BusinessName { get; private set; }
    public UserPhoneNumber PhoneNumber { get; private set; }
    public UserRole Role { get; private set; }
    
    public Profile()
    {
        Name = new UserName();
        Email = new UserEmail();
        Address = new UserBusinessAddress();
        BusinessName = new UserBusinessName();
        PhoneNumber = new UserPhoneNumber();
        Role = new UserRole();
    }
    
    public Profile(string name, string email, string address, string businessName, string phoneNumber, string role)
    {
        Name = new UserName(name);
        Email = new UserEmail(email);
        Address = new UserBusinessAddress(address);
        BusinessName = new UserBusinessName(businessName);
        PhoneNumber = new UserPhoneNumber(phoneNumber);
        Role = new UserRole(role);
    }
    
    public Profile(CreateProfileCommand command) : this(command.Name, command.Email, command.BusinessAddress, command.BusinessName, command.PhoneNumber, command.Role)
    {
        // This constructor initializes the Profile object using a CreateProfileCommand.
        // It maps the properties from the command to the Profile's value objects.
    }
}