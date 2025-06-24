using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;

namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Transform;

public static class MarkAlertAsReadCommandFromResourceAssembler
{
    public static MarkAlertAsReadCommand ToCommandFromResource(string alertId)
    {
        return new MarkAlertAsReadCommand(alertId);
    }
}