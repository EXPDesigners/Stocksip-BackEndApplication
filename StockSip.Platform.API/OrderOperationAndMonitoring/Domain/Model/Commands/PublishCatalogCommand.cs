namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

public record PublishCatalogCommand
{
    public long CatalogId { get; }

    public PublishCatalogCommand(long catalogId)
    {
        if (catalogId <= 0)
            throw new ArgumentException("CatalogId must be a non‑empty string.", nameof(catalogId));

        CatalogId = catalogId;
    }
}