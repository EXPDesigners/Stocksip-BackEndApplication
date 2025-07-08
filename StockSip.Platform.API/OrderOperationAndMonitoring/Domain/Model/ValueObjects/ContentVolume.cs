namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record ContentVolume
{
    public int Value { get; }
    
    public ContentVolume(int value)
    {
        if (value <= 0)
            throw new ArgumentException("ContentVolume must be a positive integer (ml).", nameof(value));

        Value = value;
    }

    public static implicit operator int(ContentVolume c)   => c.Value;
    public static implicit operator ContentVolume(int ml) => new(ml);

    public override string ToString() => $"{Value}ml";
}