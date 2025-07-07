using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;

public class Catalog
{
    public long CatalogId { get; internal set; } = 0;

    public AccountId AccountId { get; internal set; }

    public CatalogName Name { get; private set; }

    public DateCreated DateCreated { get; internal set; } = DateCreated.Now();

    public bool IsPublished { get; private set; }

    public IList<CatalogItem> Items { get; private set; } = new List<CatalogItem>();

    
    private Catalog() { }
    
    public Catalog(string accountId, string name)
    {
        AccountId   = new AccountId(accountId);
        Name        = new CatalogName(name);
        DateCreated = DateCreated.Now();
        IsPublished = false;
    }

    /// <summary>
    /// Creates a new catalog from a command.
    /// </summary>
    public Catalog(CreateCatalogCommand cmd) : this(cmd.AccountId, cmd.Name) { }
    

    public void Publish()
    {
        if (IsPublished)
            throw new InvalidOperationException("Catalog already published.");

        if (Items.Count == 0)
            throw new InvalidOperationException("Cannot publish an empty catalog.");

        IsPublished = true;
    }

    public void Unpublish() => IsPublished = false;

    public Catalog AddItem(CatalogItem item)
    {
        Items.Add(item);
        item.SetCatalog(this);
        return this;
    }
    
    public void Update(UpdateCatalogCommand cmd)
    {
        if (!AccountId.Equals(new AccountId(cmd.AccountId)))
            throw new InvalidOperationException("Account mismatch.");

        Name = new CatalogName(cmd.Name);
    }

    public void RemoveItem(CatalogItem item, AppDbContext ctx)
    {
        if (Items.Remove(item))
        {
            ctx.Remove(item);
        }
    }
}