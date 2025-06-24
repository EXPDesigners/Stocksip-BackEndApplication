using StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices.ACL;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Events;
using StockSip.Platform.API.Shared.Application.Internal.EventHandlers;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.EventHandlers;

/// <summary>
/// This event handler listens for ProductProblemDetectedEvent and triggers the creation of an alert
/// </summary>
/// <param name="alertsAndNotificationsService">
/// The service responsible for creating alerts and notifications on another contexts.
/// </param>
public class ProductProblemDetectedEventHandler(
    ExternalAlertsAndNotificationsService alertsAndNotificationsService
    ) : IEventHandler<ProductProblemDetectedEvent>
{
    /// <summary>
    /// Default constructor for ProductProblemDetectedEventHandler.
    /// </summary>
    /// <returns>
    /// The task representing the asynchronous operation of handling the event.
    /// </returns>
    public Task Handle(ProductProblemDetectedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    /// <summary>
    /// This method is called when a ProductProblemDetectedEvent is raised.
    /// </summary>
    /// <param name="domainEvent">
    /// The ProductProblemDetectedEvent that contains the details of the problem detected.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation of handling the event.
    /// </returns>
    private Task On(ProductProblemDetectedEvent domainEvent)
    {
        alertsAndNotificationsService.CreateAlert(
            domainEvent.Title,
            domainEvent.Message,
            domainEvent.Severity,
            domainEvent.Type,
            domainEvent.ProfileId,
            domainEvent.ProductId,
            domainEvent.WarehouseId);
        return Task.CompletedTask;
    }
}