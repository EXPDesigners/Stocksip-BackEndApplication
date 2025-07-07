using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.External;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Queries;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Application.Internal.QueryService;

public class CatalogQueryService(
        ICatalogRepository catalogRepository,
        IAccountClient     accountClient)
    : ICatalogQueryService
{

    public Task<IEnumerable<Catalog>> FindByAccountIdAsync(AccountId accountId) =>
        catalogRepository.FindByAccountIdAsync(accountId);

    public Task<IEnumerable<Catalog>> FindPublishedByAccountIdAsync(AccountId accountId) =>
        catalogRepository.FindPublishedByAccountIdAsync(accountId);

    public Task<IEnumerable<Catalog>> FindPublishedAsync() =>
        catalogRepository.FindPublishedAsync();

    public async Task<IEnumerable<Catalog>> FindAllAsync() =>
        await catalogRepository.ListAsync();

    public Task<Catalog?> FindByIdAsync(long catalogId, bool includeItems = false) =>
        catalogRepository.FindByIdAsync(catalogId, includeItems);
    

    public Task<IEnumerable<CatalogItem>> FindItemsByCatalogIdAsync(long catalogId) =>
        catalogRepository.FindByCatalogIdAsync(catalogId);

    public async Task<CatalogItem?> FindItemByIdAsync(string itemId)
    {
        var catalog = await catalogRepository.FindCatalogByItemIdAsync(itemId);
        return catalog?.Items.FirstOrDefault(i => i.Id == itemId);
    }


    public async Task<IEnumerable<Catalog>> GetPublishedCatalogsByProviderEmailAsync(string email)
    {
        var account = await accountClient.GetAccountByEmailAsync(email);
        if (account == null)
            throw new ArgumentException($"No account found for email {email}");

        // Debug: muestra exactamente qué rol llega
        Console.WriteLine($"[CatalogQuery] Email='{email}' Role='{account.Role}'");

        if (!string.Equals(account.Role, "Supplier", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("The account is not a Supplier.");

        var supplierAccountId = new AccountId(account.AccountId);
        return await catalogRepository.FindPublishedByAccountIdAsync(supplierAccountId);
    }


    public Task<IEnumerable<Catalog>> Handle(GetCatalogsByAccountQuery q) =>
        FindByAccountIdAsync(q.AccountId);

    public Task<IEnumerable<Catalog>> Handle(GetPublishedCatalogsByAccountIdQuery q) =>
        FindPublishedByAccountIdAsync(q.AccountId);

    public Task<IEnumerable<Catalog>> Handle(GetPublishedCatalogsQuery q) =>
        FindPublishedAsync();

    public Task<IEnumerable<Catalog>> Handle(GetAllCatalogsQuery _) =>
        FindAllAsync();

    public Task<Catalog?> Handle(GetCatalogByIdQuery q) =>
        FindByIdAsync(q.CatalogId);

    public Task<IEnumerable<CatalogItem>> Handle(GetCatalogItemsByCatalogIdQuery q) =>
        FindItemsByCatalogIdAsync(q.CatalogId);

    public Task<CatalogItem?> Handle(GetCatalogItemByIdQuery q) =>
        FindItemByIdAsync(q.CatalogItemId);
}