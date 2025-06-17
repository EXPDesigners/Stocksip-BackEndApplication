using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.CommandService;

/// <summary>
/// This class implements the command service for handling warehouse-related commands.
/// </summary>
/// <param name="warehouseRepository">The repository for managing warehouse data.</param>
/// <param name="unitOfWork">The unit of work for managing transactions.</param>
public class WarehouseCommandService(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork) : IWarehouseCommandService
{
    /// <summary>
    /// This method handles the creation of a new warehouse.
    /// </summary>
    /// <param name="command">The command containing the details for creating a warehouse.</param>
    /// <returns> The created warehouse or null if the creation fails.</returns>
    /// <exception cref="ArgumentException"> Thrown when a warehouse with the same name or address already exists.</exception>
    public async Task<Warehouse?> Handle(CreateWarehouseCommand command)
    {
        if (await warehouseRepository.ExistByNameIgnoreCaseAndProfileIdAsync(command.Name, command.ProfileId))
        {
            throw new ArgumentException($"Warehouse with name {command.Name} already exists.");
        }

        if (await warehouseRepository.ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileId(
                command.Street, command.City, command.PostalCode, command.ProfileId))
        {
            throw new ArgumentException($"Warehouse with address {command.Street}, {command.City}, {command.Country} already exists.");
        }
        
        var warehouse = new Warehouse(command);
        await warehouseRepository.AddAsync(warehouse);
        await unitOfWork.CompleteAsync();
        return warehouse;
    }
}