namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// This value object represents the profile ID of a warehouse.
/// <summary>
/// This record defines the address of a warehouse.
/// </summary>
public record WarehouseAddress()
{
    private string  Street { get; }
    private string City { get; }
    private string District { get; }
    private string PostalCode { get; }
    private string Country { get; }

    /// <summary>
    /// The default constructor for the WarehouseAddress record.
    /// </summary>
    /// <param name="street">The street for the warehouse</param>
    /// <param name="city">The city for the warehouse</param>
    /// <param name="district">The district for the warehouse</param>
    /// <param name="postalCode">The postal code for the warehouse</param>
    /// <param name="country">The country for the warehouse</param>
    /// <exception cref="ArgumentException">The warehouse address cannot be null or empty</exception>
    public WarehouseAddress(string street, string city, string district, string postalCode, string country) : this()
    {
        if (!IsValidAddress(street, city, district, postalCode, country))
        {
            throw new ArgumentException("Invalid warehouse address. All fields must be provided and cannot be empty.");
        }
        Street = street;
        City = city;
        District = district;
        PostalCode = postalCode;
        Country = country;
    }
    
    /// <summary>
    /// Validates the warehouse address.
    /// </summary>
    /// <returns>True if the address is valid, otherwise false.</returns>
    private static bool IsValidAddress(string street, string city, string district, string postalCode, string country)
    {
        return !string.IsNullOrWhiteSpace(street) &&
               !string.IsNullOrWhiteSpace(city) &&
               !string.IsNullOrWhiteSpace(district) &&
               !string.IsNullOrWhiteSpace(postalCode) &&
               !string.IsNullOrWhiteSpace(country);
    }
}
