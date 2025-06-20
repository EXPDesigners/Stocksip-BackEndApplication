using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Repositories;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.AlertsAndNotifications.Application.Internal.CommandServices;

/// <summary>
/// This class implements the command service for handling alert-related commands.
/// </summary>
/// <param name="alertRepository">
/// The repository for managing alerts.
/// </param>
/// <param name="unitOfWork">
/// The unit of work for managing transactions.
/// </param>
public class AlertCommandService(
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork
    ) : IAlertCommandService
{
    /// <summary>
    /// This async method handles the creation of a new alert based on the provided command.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for creating a new alert.
    /// </param>
    /// <returns>
    /// The created alert object, or null if the creation fails.
    /// </returns>
    public async Task<Alert?> Handle(CreateAlertCommand command)
    {
        var alert = new Alert(command);
        await alertRepository.AddAsync(alert);
        await unitOfWork.CompleteAsync();
        return alert;
    }

    /// <summary>
    /// This async method handles the marking of an alert as resolved based on the provided command.
    /// </summary>
    /// <param name="command">
    /// The command containing the ID of the alert to be marked as resolved.
    /// </param>
    /// <returns>
    /// The updated alert object after marking it as resolved, or null if the alert does not exist.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the alert with the specified ID does not exist in the repository.
    /// </exception>
    public async Task<Alert?> Handle(MarkAlertAsResolvedCommand command)
    {
        var alertToMark = await alertRepository.FindByIdAsync(command.AlertId)
                          ?? throw new ArgumentException($"Alert with ID {command.AlertId} does not exist.");
        
        alertToMark.Resolve();
        alertRepository.Update(alertToMark);
        await unitOfWork.CompleteAsync();
        return alertToMark;
    }
}