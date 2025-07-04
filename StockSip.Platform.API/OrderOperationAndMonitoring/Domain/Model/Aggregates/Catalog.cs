using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;

public class Catalog
{
    public int CatalogId { get; internal set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsPublished { get; set; }
    public AccountId AccountId { get; set; }

    private Catalog() { }
    
    public Catalog(string name, DateTime dateCreated, bool isPublished, AccountId accountId)
    {
        Name = name;
        DateCreated = dateCreated;
        IsPublished = isPublished;
        AccountId = accountId;
    }

    public Catalog(CreateCatalogCommand command) : this(command.Name,
        new DateCreated(command.DateCreatedAt).Value,
        new IsPublished(command.IsPublished),
        new AccountId(command.AccountId)) 
    {}
    
    public void Update(CreateCatalogCommand command)
    {
        Name = command.Name;
        DateCreated = new DateCreated(command.DateCreatedAt).Value;
        IsPublished = new IsPublished(command.IsPublished);
        AccountId = new AccountId(command.AccountId);
    }

}