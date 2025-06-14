namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// Temperature value object that represents the temperature range for a warehouse.
/// <summary>
/// This record defines a temperature range with a maximum and minimum temperature.
/// </summary>
public record Temperature()
{
    /// <summary>
    /// The maximum allowed temperature for a warehouse.
    /// </summary>
    private const double MaxAllowedTemperature = 40.0;
    private const double MinAllowedTemperature = -20.0;
    
    /// <summary>
    /// The maximum temperature for the warehouse.
    /// </summary>
    private double MaxTemperature { get; }
    private double MinTemperature { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Temperature"/> record with default values.
    /// </summary>
    /// <param name="maxTemperature">The maximum temperature of a warehouse</param>
    /// <param name="minTemperature">The minimum temperature of a warehouse</param>
    /// <exception cref="ArgumentOutOfRangeException">Validates the allowed temperature</exception>
    public Temperature(double maxTemperature, double minTemperature) : this()
    {
        if (!IsValidTemperature(maxTemperature, minTemperature))
        {
            throw new ArgumentOutOfRangeException($"Max temperature must be less than or equal to {MaxAllowedTemperature} and Min temperature must be greater than or equal to {MinAllowedTemperature}.");
        }

        MaxTemperature = maxTemperature;
        MinTemperature = minTemperature;
    }
    
    /// <summary>
    /// This method checks if the provided maximum and minimum temperatures are valid.
    /// </summary>
    /// <param name="maxTemperature">A double that represents the max temperature</param>
    /// <param name="minTemperature">A double that represents the min temperature</param>
    /// <returns>A boolean</returns>
    private static bool IsValidTemperature(double maxTemperature, double minTemperature)
    {
        if (maxTemperature < minTemperature && maxTemperature > MaxAllowedTemperature && maxTemperature < MinAllowedTemperature)
        {
            return false;
        }
        
        if (minTemperature < MaxAllowedTemperature && minTemperature > MinAllowedTemperature && minTemperature < maxTemperature)
        {
            return false;
        }
        
        return true;
    }
}