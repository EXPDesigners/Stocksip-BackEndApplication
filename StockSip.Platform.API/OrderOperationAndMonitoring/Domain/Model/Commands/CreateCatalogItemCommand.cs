namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

/// <summary>
/// Command to create a new catalog item.
/// </summary>
public record CreateCatalogItemCommand
{
    public long CatalogId { get; }
    public string Name { get; }
    public string ProductType { get; }
    public string Brand { get; }
    public int Content { get; }
    public decimal UnitPrice { get; }

    public CreateCatalogItemCommand(
        long catalogId,
        string name,
        string productType,
        string brand,
        int content,
        decimal unitPrice)
    {
        if (catalogId <= 0)
            throw new ArgumentException("CatalogId is required.", nameof(catalogId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(productType))
            throw new ArgumentException("ProductType is required.", nameof(productType));

        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand is required.", nameof(brand));

        if (content <= 0)
            throw new ArgumentException("Content must be greater than 0.", nameof(content));

        if (unitPrice <= 0)
            throw new ArgumentException("UnitPrice must be greater than 0.", nameof(unitPrice));

        CatalogId = catalogId;
        Name = name;
        ProductType = productType;
        Brand = brand;
        Content = content;
        UnitPrice = unitPrice;
    }
}