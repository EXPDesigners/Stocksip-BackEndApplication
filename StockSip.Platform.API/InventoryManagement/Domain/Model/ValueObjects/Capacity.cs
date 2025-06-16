namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// The capacity value object that represents the total capacity of a warehouse.
/// <summary>
/// This record defines the total capacity of a warehouse.
/// </summary>
public record Capacity()
{
    
    /// <summary>
    /// This property defines the maximum allowed capacity for a warehouse.
    /// </summary>
    public double TotalCapacity { get; }

    /// <summary>
    /// Default constructor for the Capacity record.
    /// </summary>
    /// <param name="totalCapacity">The physical capacity in the warehouse</param>
    /// <exception cref="ArgumentOutOfRangeException">The total capacity must be greater than zero</exception>
    public Capacity(double totalCapacity) : this()
    {
        IsValidCapacity(totalCapacity);
        TotalCapacity = totalCapacity;
    }
    
    /// <summary>
    /// This method checks if the provided total capacity is valid.
    /// </summary>
    /// <param name="totalCapacity">The physical capacity in the warehouse</param>
    /// <returns>A true is the capacity is valid, otherwise false</returns>
    private static bool IsValidCapacity(double totalCapacity)
    {
        if (totalCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalCapacity), "The total capacity must be greater than zero.");
        }
        return true;
    }
}