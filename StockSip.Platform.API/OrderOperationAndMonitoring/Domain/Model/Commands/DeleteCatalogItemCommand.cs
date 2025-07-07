namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

public record DeleteCatalogItemCommand
{
    public string CatalogItemId { get; }

    public DeleteCatalogItemCommand(string catalogItemId)
    {
        if (string.IsNullOrWhiteSpace(catalogItemId))
            throw new ArgumentException("CatalogItemId is required.", nameof(catalogItemId));

        CatalogItemId = catalogItemId;
    }
}