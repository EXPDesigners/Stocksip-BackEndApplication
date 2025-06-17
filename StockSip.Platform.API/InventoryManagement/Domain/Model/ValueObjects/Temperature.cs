namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// Temperature value object that represents the temperature range for a warehouse.
/// <summary>
/// This record defines a temperature range with a maximum and minimum temperature.
/// </summary>
public record Temperature()
{
    /*
     * The minimum temperature limit for the warehouse.
     */
    private const double MinTemperatureLimit = -50.0;

    /*
     * The maximum temperature limit for the warehouse.
     */
    private const double MaxTemperatureLimit = 50.0;

    /// <summary>
    /// The minimum temperature.
    /// </summary>
    public double MinTemperature { get; }
    
    /// <summary>
    /// The maximum temperature.
    /// </summary>
    public double MaxTemperature { get; }

    /// <summary>
    /// The minimum temperature.
    /// </summary>
    /// <param name="minTemperature">The minimum temperature</param>
    /// <param name="maxTemperature">The maximum temperature</param>
    public Temperature(double minTemperature, double maxTemperature) : this()
    {
        Console.WriteLine($"Received min: {minTemperature}, max: {maxTemperature}");
        ValidateTemperature(minTemperature, maxTemperature);
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
    }

    /// <summary>
    /// This method validates the temperature range.
    /// </summary>
    /// <param name="minTemperature">The minimum temperature</param>
    /// <param name="maxTemperature">The maximum temperature</param>
    /// <exception cref="ArgumentException">Validates the allowed temperature</exception>
    private static void ValidateTemperature(double minTemperature, double maxTemperature)
    {
        if (minTemperature >= maxTemperature)
        {
            throw new ArgumentException("The maximum temperature must be greater than the minimum temperature.");
        }

        if (minTemperature is < MinTemperatureLimit or > MaxTemperatureLimit)
        {
            throw new ArgumentException(
                $"The minimum temperature must be between {MinTemperatureLimit} and {MaxTemperatureLimit} degrees Celsius.");
        }

        if (maxTemperature is < MinTemperatureLimit or > MaxTemperatureLimit)
        {
            throw new ArgumentException(
                $"The maximum temperature must be between {MinTemperatureLimit} and {MaxTemperatureLimit} degrees Celsius.");
        }
    }
}