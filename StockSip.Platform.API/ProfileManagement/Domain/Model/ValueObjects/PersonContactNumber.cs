namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the contact number of a person.
/// </summary>
public record PersonContactNumber()
{
    /// <summary>
    /// The phone number associated with the person.
    /// </summary>
    public string PhoneNumber { get; } = string.Empty;
    
    /// <summary>
    /// This constructor initializes a new instance of the <see cref="PersonContactNumber"/> class with the specified phone number.
    /// </summary>
    /// <param name="phoneNumber"></param>
    public PersonContactNumber(string phoneNumber) : this()
    {
        PhoneNumber = phoneNumber;
    }
}