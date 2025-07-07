using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// Represents an item in a catalog, which includes product details such as name, type, brand, content volume, and price.
/// </summary>
public class CatalogItem
{
    [Key]
    [MaxLength(36)]
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    
    public long   CatalogId { get; internal set; }
    public Catalog? Catalog { get; internal set; }

    public ProductName  Name        { get; private set; } = default!;
    public ProductType  ProductType { get; private set; } = default!;
    public BrandName    Brand       { get; private set; } = default!;
    public ContentVolume Content    { get; private set; } = default!;


    [Column(TypeName = "decimal(12,2)")]
    public decimal UnitPrice { get; private set; }

    public DateTime DateAdded { get; private set; } = DateTime.UtcNow;
    
    private CatalogItem() { }
    
    public CatalogItem(CreateCatalogItemCommand cmd)
    {
        Name        = new ProductName(cmd.Name);
        ProductType = new ProductType(cmd.ProductType);
        Brand       = new BrandName(cmd.Brand);
        Content     = new ContentVolume(cmd.Content);
        UnitPrice   = cmd.UnitPrice;
        DateAdded   = DateTime.UtcNow;
    }
    
    internal void SetCatalog(Catalog? catalog)
    {
        Catalog   = catalog;
        CatalogId = catalog?.CatalogId ?? 0;
    }
}
