using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.CommandService;

/// <summary>
/// This class implements the command service for handling care guide related commands.
/// </summary>
/// <param name="careGuideRepository">
/// The repository for managing care guide data. 
/// </param>
/// <param name="unitOfWork">
/// The unit of work for managing transactions.
/// </param>
public class CareGuideCommandService(
    ICareGuideRepository careGuideRepository,
    IUnitOfWork unitOfWork
    ) : ICareGuideCommandService
{
    /// <summary>
    /// This async method is used to create a care guide assigning it to a specific product.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create a new care guide assigning it to a product. 
    /// </param>
    /// <returns>
    /// The created care guide object.
    /// </returns>
    public async Task<CareGuide?> Handle(CreateCareGuideCommand command)
    {
        var careGuide = new CareGuide(command);
        await careGuideRepository.AddAsync(careGuide);
        await unitOfWork.CompleteAsync();
        return careGuide;
    }

    /// <summary>
    /// This async method is used to create a new Care guide object without assigning it to a specific product.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for creating a new care guide without assigning it to a product.
    /// </param>
    /// <returns>
    /// The created care guide.
    /// </returns>
    public async Task<CareGuide?> Handle(CreateCareGuideWithoutProductIdCommand command)
    {
        var careGuide = new CareGuide(command.AccountId, command.Title, command.Summary, command.Summary,
            command.MinTemp, command.MaxTemp, command.PlaceStorage, command.Recommendation);
        await careGuideRepository.AddAsync(careGuide);
        await unitOfWork.CompleteAsync();
        return careGuide;
    }

    /// <summary>
    /// This async method is used to update the recommendations of the current care guide.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for updating the recommendations of the care guide.
    /// </param>
    /// <returns>
    /// The updated care guide.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when a care guide with the specific id does not exist.
    /// </exception>
    public async Task<CareGuide?> Handle(UpdateCareGuideCommand command)
    {
        var careGuideToUpdate = await careGuideRepository.FindByIdAsync(command.CareGuideId) 
                                ?? throw new ArgumentException("Care guide with given id does not exists.");
        
        careGuideToUpdate.UpdateRecommendations(command.NewTitle, command.NewSummary, command.NewMinTemp, command.NewMaxTemp, command.NewPlaceStorage, command.NewRecommendation);
        careGuideRepository.Update(careGuideToUpdate);
        await unitOfWork.CompleteAsync();
        return careGuideToUpdate;
    }

    /// <summary>
    /// This async is used to unassign the care guide from the current product.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for unassigning a care guide.
    /// </param>
    /// <returns>
    /// The unassigned care guide.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when a care guide with the specific id does not exist.
    /// </exception>
    public async Task<CareGuide?> Handle(UnassignCareGuideCommand command)
    {
        var careGuideToUnassign = await careGuideRepository.FindByIdAsync(command.CareGuideId)
                                  ?? throw new ArgumentException("Care guide with given id does not exists.");
        
        careGuideToUnassign.UnassignCareGuide();
        careGuideRepository.Update(careGuideToUnassign);
        await unitOfWork.CompleteAsync();
        return careGuideToUnassign;
    }

    /// <summary>
    /// This method is used to assign a care guide to a specific product.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for assign a care guide to a specific product.
    /// </param>
    /// <returns>
    /// The assigned care guide.
    /// ></returns>
    /// <exception cref="ArgumentException">
    /// Thrown when a care guide with the specific id does not exist.
    /// </exception>
    public async Task<CareGuide?> Handle(AssignCareGuideToProductCommand command)
    {
        var careGuideToAssign = await careGuideRepository.FindByIdAsync(command.CareGuideId)
                                  ?? throw new ArgumentException("Care guide with given id does not exists.");
        
        careGuideToAssign.AssignCareGuideToAnotherProduct(command.ProductId);
        careGuideRepository.Update(careGuideToAssign);
        await unitOfWork.CompleteAsync();
        return careGuideToAssign;
    }

    /// <summary>
    /// This async method is used to delete a careguide.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to delete a care guide.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when a care guide with the specific id does not exist. 
    /// </exception>
    public async Task Handle(DeleteCareGuideCommand command)
    {
        var careGuideToDelete = await careGuideRepository.FindByIdAsync(command.CareGuideId)
                                ?? throw new ArgumentException("Care Guide with give id does not exists.");
        
        careGuideRepository.Remove(careGuideToDelete);
        await unitOfWork.CompleteAsync();
    }
}