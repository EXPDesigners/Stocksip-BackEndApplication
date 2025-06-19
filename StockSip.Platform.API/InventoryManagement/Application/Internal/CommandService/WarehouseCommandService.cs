using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.CommandService;

/// <summary>
/// This class implements the command service for handling warehouse-related commands.
/// </summary>
/// <param name="warehouseRepository">The repository for managing warehouse data.</param>
/// <param name="unitOfWork">The unit of work for managing transactions.</param>
public class WarehouseCommandService(
    IWarehouseRepository warehouseRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IWarehouseCommandService
{
    /// <summary>
    /// This method handles the creation of a new warehouse.
    /// </summary>
    /// <param name="command">The command containing the details for creating a warehouse.</param>
    /// <returns> The created warehouse or null if the creation fails.</returns>
    /// <exception cref="ArgumentException"> Thrown when a warehouse with the same name or address already exists.</exception>
    public async Task<Warehouse?> Handle(CreateWarehouseCommand command)
    {
        if (await warehouseRepository.ExistByNameIgnoreCaseAndProfileIdAsync(command.Name, new ProfileId(command.ProfileId)))
        {
            throw new ArgumentException($"Warehouse with name {command.Name} already exists.");
        }

        if (await warehouseRepository.ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAsync(
                command.Street, command.City, command.PostalCode, new ProfileId(command.ProfileId)))
        {
            throw new ArgumentException($"Warehouse with address {command.Street}, {command.City}, {command.PostalCode} already exists.");
        }
        
        var warehouse = new Warehouse(command);
        await warehouseRepository.AddAsync(warehouse);
        await unitOfWork.CompleteAsync();
        return warehouse;
    }
    
    /// <summary>
    /// This method handles the update of an existing warehouse.
    /// </summary>
    /// <param name="command">The command containing the details for updating a warehouse.</param>
    /// <returns>The updated warehouse or null if the update fails.</returns>
    /// <exception cref="ArgumentException">Thrown when a warehouse with the same name or address already exists, or if the warehouse to update does not exist.</exception>
    public async Task<Warehouse?> Handle(UpdateWarehouseCommand command)
    {
        var warehouseToUpdate = await warehouseRepository.FindByIdAsync(command.WarehouseId)
            ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");

        if (await warehouseRepository.ExistsByNameIgnoreCaseAndProfileIdAndWarehouseIdIsNotAsync(
                command.Name, command.ProfileId, command.WarehouseId))
        {
            throw new ArgumentException($"Warehouse with name {command.Name} already exists.");
        }
        
        if (await warehouseRepository.ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAndProfileIdIsNotAsync(
                command.Street, command.City, command.PostalCode, command.ProfileId, command.WarehouseId))
        {
            throw new ArgumentException($"Warehouse with address {command.Street}, {command.City}, {command.PostalCode} already exists.");
        }
        
        warehouseToUpdate.UpdateWarehouse(
            command.Name,
            command.Street,
            command.City,
            command.District,
            command.PostalCode,
            command.Country,
            command.MaxTemperature,
            command.MinTemperature,
            command.Capacity
        );
        
        warehouseRepository.Update(warehouseToUpdate);
        await unitOfWork.CompleteAsync();
        return warehouseToUpdate;
    }

    /// <summary>
    /// This async method handles the registration of a product exit from a warehouse.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for registering a product exit.
    /// </param>
    /// <returns>
    /// The registered product exit or null if the registration fails.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the warehouse, product, or inventory does not exist.
    /// </exception>
    public async Task<ProductExit?> Handle(RegisterProductExitCommand command)
    {
        var warehouse = await warehouseRepository.FindByIdAsync(command.WarehouseId)
                        ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");
        var product = await productRepository.FindByIdAsync(command.ProductId)
                        ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        var inventory = await productRepository.FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(command.ProductId, command.WarehouseId, command.ExpirationDate)
                        ?? throw new ArgumentException($"Inventory for product {command.ProductId} in warehouse {command.WarehouseId} does not exist.");
        
        var productExit = new ProductExit(command)
        {
            Inventory = inventory
        };
        await unitOfWork.CompleteAsync();
        return productExit;
    }

    /// <summary>
    /// This async method handles the deletion of a warehouse.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for deleting a warehouse.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the warehouse to delete does not exist.
    /// </exception>
    public async Task Handle(DeleteWarehouseCommand command)
    {
        var warehouseToDelete = await warehouseRepository.FindByIdAsync(command.WarehouseId)
                                ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");
        
        warehouseRepository.Remove(warehouseToDelete);
        await unitOfWork.CompleteAsync();
    }
}