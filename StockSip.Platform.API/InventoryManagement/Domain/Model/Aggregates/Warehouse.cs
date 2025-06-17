using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

/// This aggregate root represents a warehouse in the inventory management system.
/// <summary>
/// This class defines the properties and behaviors of a warehouse, including its name, address, temperature range, capacity, and associated profile ID.
/// </summary>
public class Warehouse
{
    public int WarehouseId { get; internal set; }
    public string? Name { get; private set; }
    public WarehouseAddress? Address { get; internal set; }
    public Temperature? Temperature { get; internal set; }
    public Capacity? Capacity { get; internal set; }
    
    public ImageUrl? ImageUrl { get; internal set;  }
    
    public ProfileId ProfileId { get; internal set; }
    
    private Warehouse() { }

    /// <summary>
    /// The constructor initializes a new instance of the Warehouse class with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the warehouse</param>
    /// <param name="address">The address of the warehouse</param>
    /// <param name="temperature">The address of the warehouse</param>
    /// <param name="capacity">The capacity of the warehouse</param>
    /// <param name="imageUrl">The image of the warehouse</param>
    /// <param name="profileId">The profile to which the warehouse belongs</param>
    public Warehouse(string name, WarehouseAddress address, Temperature temperature, Capacity capacity,
        ImageUrl imageUrl, ProfileId profileId)
    {
        
        Name = ValidateName(name);
        Address = address;
        Temperature = temperature;
        Capacity = capacity;
        ImageUrl = imageUrl;
        ProfileId = profileId;
    }
    
    /// <summary>
    /// Constructs a new instance of the Warehouse class using a CreateWarehouseCommand.
    /// </summary>
    /// <param name="command">The command to create a warehouse</param>
    public Warehouse(CreateWarehouseCommand command) : this(
        command.Name,
        new WarehouseAddress(command.Street, command.City, command.District, command.PostalCode, command.Country),
        new Temperature(command.MinTemperature, command.MaxTemperature),
        new Capacity(command.Capacity),
        new ImageUrl(null),
        new ProfileId(command.ProfileId)) 
    {}

    /// <summary>
    /// Constructs a new instance of the Warehouse class using an UpdateWarehouseCommand.
    /// </summary>
    /// <param name="command"></param>
    public void UpdateWarehouse(UpdateWarehouseCommand command)
    {
        Name = ValidateName(command.Name);
        Address = command.Address;
        Temperature = command.Temperature;
        Capacity = command.Capacity;
        ImageUrl = new ImageUrl(command.ImageUrl);
        ProfileId = new ProfileId(command.ProfileId);
    }
    
    /// <summary>
    /// This method validates the warehouse name.
    /// </summary>
    /// <exception cref="ArgumentException">The name cannot be null or empty</exception>
    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The warehouse name cannot be null or empty.", nameof(name));

        return name;
    }
}