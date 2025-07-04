namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the person's full name.
/// </summary>
public record PersonName(string FirstName, string LastName)
{
    /// <summary>
    /// This constructor initializes a new instance of the <see cref="PersonName"/> class with empty first and last names.
    /// </summary>
    public PersonName() : this(string.Empty, string.Empty) { }
    
    /// <summary>
    /// This constructor initializes a new instance of the <see cref="PersonName"/> class with the specified first and last names.
    /// </summary>
    /// <param name="firstName"></param>
    public PersonName(string firstName) : this(firstName, string.Empty) { }
    
    /// <summary>
    /// This property returns the full name by combining the first and last names.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();
}