using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.QueryService;

/// <summary>
/// This class implements the IProductQueryService interface to handle queries related to products.
/// </summary>
/// <param name="productRepository"> The repository for accessing to product data. </param>
public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    /// <summary>
    /// This method retrieves all products associated with a specific provider ID and a warehouse ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the provider ID for which products are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of products associated with the specified provider ID and warehouse ID.
    /// </returns>
    public async Task<IEnumerable<Product>> Handle(GetAllProductsByProviderIdAndWarehouseIdQuery query)
    {
        return await productRepository.FindByProviderIdAndWarehouseIdAsync(query.ProviderId, query.WarehouseId);
    }

    /// <summary>
    /// Method to retrieve all products associated with a specific warehouse ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the warehouse ID for which products are to be retrieved.
    /// </param>
    /// <returns>
    /// The list of products associated with the specified warehouse ID.
    /// </returns>
    public async Task<IEnumerable<Product>> Handle(GetAllProductsByWarehouseIdQuery query)
    {
        return await productRepository.FindByWarehouseIdAsync(query.WarehouseId); 
    }

    /// <summary>
    /// Method to retrieve a product by its ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the product ID for which the product is to be retrieved.
    /// </param>
    /// <returns>
    /// A task that returns the product if found, or null if not found.
    /// </returns>
    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.FindByIdAsync(query.ProductId);
    }

    /// <summary>
    /// This async method retrieves all inventory items that match the specified full name and warehouse ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the full name of the product and the warehouse ID for which inventory items are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of inventory items that match the specified full name and warehouse ID.
    /// </returns>
    public async Task<IEnumerable<Product>> Handle(GetProductsByFullNameAndWarehouseIdQuery query)
    {
        return await productRepository.FindByFullNameAndWarehouseId(query.BrandName, query.LiquorType,
            query.AdditionalName, query.WarehouseId);
    }

    /// <summary>
    /// This async method retrieves all products associated with a specific profile ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the profile ID for which products are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of inventory items associated with the specified profile ID.
    /// </returns>
    public async Task<IEnumerable<Product>> Handle(GetAllProductsByProfileIdQuery query)
    {
        return await productRepository.FindProductsByProfileIdAsync(query.ProfileId);
    }

    /// <summary>
    /// This async method retrieves an inventory item by its product ID, warehouse ID, and expiration date.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<Product?> Handle(GetProductByIdAndWarehouseIdAndExpirationDateQuery query)
    {
        return await productRepository.FindByProductIdAndWarehouseIdAndExpirationDateAsync(query.ProductId,
            query.WarehouseId, query.ExpirationDate);
    }
}