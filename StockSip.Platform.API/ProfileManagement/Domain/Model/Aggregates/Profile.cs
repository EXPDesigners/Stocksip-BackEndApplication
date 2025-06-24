using StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;
using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;

/// <summary>
/// This class represents a user profile in the system.
/// </summary>
public class Profile
{
    /// <summary>
    /// The unique identifier for the profile.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();
    
    private PersonName Name { get; set; }
    private PersonContactNumber PersonContactNumber { get; set; }
    
    /// <summary>
    /// The full name of the profile, derived from the person's name.
    /// </summary>
    public string FullName => Name.FullName;

    /// <summary>
    /// The contact number of the profile, derived from the person's contact number.
    /// </summary>
    public string ContactNumber => PersonContactNumber.PhoneNumber;
    
    /// <summary>
    /// The url of the profile picture associated with this profile.
    /// </summary>
    public string ProfilePictureUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// The unique identifier for the user associated with this profile.
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// Default constructor for the Profile class.
    /// </summary>
    public Profile()
    {
        Name = new PersonName();
        PersonContactNumber = new PersonContactNumber();
    }
    
    /// <summary>
    /// Constructor for the Profile class that initializes the profile with a first name, last name, and user ID.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="userId"></param>
    public Profile(string firstName, string lastName, string phoneNumber, UserId userId)
    {
        Name = new PersonName(firstName, lastName);
        PersonContactNumber = new PersonContactNumber(phoneNumber);
        UserId = userId;
    }

    /// <summary>
    /// Command handler constructor for creating a profile from a command.
    /// </summary>
    /// <param name="command">
    /// The command containing the necessary data to create a profile.
    /// </param>
    public Profile(CreateProfileCommand command) : this(command.FirstName, command.LastName, command.ContactNumber,
        command.UserId) { }
}