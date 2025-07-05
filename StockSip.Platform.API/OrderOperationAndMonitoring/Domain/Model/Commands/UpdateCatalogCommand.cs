namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

public record UpdateCatalogCommand
{
    public long CatalogId  { get; }
    public string AccountId  { get; }
    public string Name       { get; }

    public UpdateCatalogCommand(long catalogId, string accountId, string name)
    {
        if (catalogId <= 0)
            throw new ArgumentException("CatalogId is required.", nameof(catalogId));

        if (string.IsNullOrWhiteSpace(accountId))
            throw new ArgumentException("AccountId is required.", nameof(accountId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        CatalogId = catalogId;
        AccountId = accountId;
        Name      = name;
    }
}