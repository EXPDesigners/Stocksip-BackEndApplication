namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record DateCreated(string DateCreatedString)
{
    public DateTime Value { get; init; } = DateTime.Parse(DateCreatedString, null, System.Globalization.DateTimeStyles.RoundtripKind);

    public static implicit operator DateCreated(string dateTimeString) => new(dateTimeString);

    public static implicit operator string(DateCreated dateCreated) => dateCreated.Value.ToString("o");

    public override string ToString() => Value.ToString("o");
}
