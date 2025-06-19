using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Services;

/// <summary>
/// This interface defines the contract for services that handle commands related to warehouse operations.
/// </summary>
public interface IWarehouseCommandService
{
    /// <summary>
    /// This method handles the creation of a new warehouse based on the provided command.
    /// </summary>
    /// <param name="command">The command containing the details required to create a new warehouse.</param>
    /// <returns>A task that represents the asynchronous operation, containing the created Warehouse object or null if creation failed.</returns>
    Task<Warehouse?> Handle(CreateWarehouseCommand command);
    
    /// <summary>
    /// This method handles the update of an existing warehouse based on the provided command.
    /// </summary>
    /// <param name="command">The command containing the details required to update an existing warehouse.</param>
    /// <returns>A task that represents the asynchronous operation, containing the updated Warehouse object or null if the update failed.</returns>
    Task<Warehouse?> Handle(UpdateWarehouseCommand command);
    
    /// <summary>
    /// This method handles the deletion of a warehouse based on the provided command.
    /// </summary>
    /// <param name="command">The command containing the details required to delete a warehouse.</param>
    Task Handle(DeleteWarehouseCommand command);
}