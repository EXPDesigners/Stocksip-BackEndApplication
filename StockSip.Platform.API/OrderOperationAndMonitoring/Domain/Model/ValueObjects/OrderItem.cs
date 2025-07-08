namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public sealed class OrderItem
{
    public Guid     Id            { get; }
    public long     CatalogId     { get; }
    public string   Name          { get; }
    public string   ProductType   { get; }
    public string   Brand         { get; }
    public int      Content       { get; }
    public decimal  UnitPrice     { get; }
    public DateTime DateAdded     { get; }
    public int      CustomQuantity{ get; }
    
    private OrderItem() { }

    public OrderItem(Guid id, long catalogId, string name, string productType,
        string brand, int content, decimal unitPrice,
        DateTime dateAdded, int customQuantity)
    {
        Id             = id;
        CatalogId      = catalogId;
        Name           = name;
        ProductType    = productType;
        Brand          = brand;
        Content        = content;
        UnitPrice      = unitPrice;
        DateAdded      = dateAdded;
        CustomQuantity = customQuantity;
    }

    public decimal TotalPrice => UnitPrice * CustomQuantity;
}