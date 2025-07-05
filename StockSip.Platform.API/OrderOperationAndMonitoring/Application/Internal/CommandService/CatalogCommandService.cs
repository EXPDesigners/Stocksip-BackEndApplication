using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;
namespace StockSip.Platform.API.OrderOperationAndMonitoring.Application.Internal.CommandService;

public class CatalogCommandService(
    ICatalogRepository catalogRepository,
    IUnitOfWork        unitOfWork)
    : ICatalogCommandService
{
    public async Task<Catalog> Handle(CreateCatalogCommand cmd)
    {
        var accountIdVo = new AccountId(cmd.AccountId);

        var duplicated = (await catalogRepository.FindByAccountIdAsync(accountIdVo))
            .Any(c => c.Name.Value.Equals(cmd.Name, StringComparison.OrdinalIgnoreCase));

        if (duplicated)
            throw new ArgumentException(
                $"Catalog '{cmd.Name}' already exists for account {cmd.AccountId}."
            );

        var catalog = new Catalog(cmd);
        await catalogRepository.AddAsync(catalog);
        await unitOfWork.CompleteAsync();
        return catalog;
    }

    public async Task<Catalog?> Handle(UpdateCatalogCommand cmd)
    {
        var catalog = await catalogRepository.FindByIdAsync(cmd.CatalogId);
        if (catalog is null) return null;

        catalog.Update(cmd);
        await unitOfWork.CompleteAsync();
        return catalog;
    }

    public async Task<Catalog?> Handle(string catalogId, UpdateCatalogCommand cmd)
    {
        var catalog = await catalogRepository.FindByIdAsync(catalogId);
        if (catalog is null) return null;

        catalog.Update(cmd);
        await unitOfWork.CompleteAsync();
        return catalog;
    }

    public async Task<Catalog?> Handle(PublishCatalogCommand cmd)
    {
        var catalog = await catalogRepository.FindByIdAsync(cmd.CatalogId, includeItems: true);
        if (catalog is null) return null;

        catalog.Publish();              // Throws if already published or empty
        await unitOfWork.CompleteAsync();
        return catalog;
    }


    public async Task<CatalogItem?> Handle(CreateCatalogItemCommand cmd)
    {
        var catalog = await catalogRepository.FindByIdAsync(cmd.CatalogId, includeItems: true);
        if (catalog is null) return null;
        if (catalog.IsPublished)
            throw new InvalidOperationException("Cannot add items to a published catalog.");

        var item = new CatalogItem(cmd);
        catalog.AddItem(item);

        await unitOfWork.CompleteAsync();
        return item;
    }

    public async Task<bool> Handle(DeleteCatalogItemCommand cmd)
    {
        var catalog = await catalogRepository.FindCatalogByItemIdAsync(cmd.CatalogItemId);
        if (catalog is null || catalog.IsPublished) return false;

        var item = catalog.Items.First(i => i.Id == cmd.CatalogItemId);
        catalog.RemoveItem(item);

        await unitOfWork.CompleteAsync();
        return true;
    }
}