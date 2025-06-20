using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Services;

public interface IAlertCommandService
{
    Task<Alert?> Handle(CreateAlertCommand command);
    Task<Alert?> Handle(MarkAlertAsResolvedCommand command);
}