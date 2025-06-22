using System.Collections;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Services;

public interface IAlertQueryService
{
    Task<Alert?> Handle(GetAlertByIdQuery query);
    
    Task<IEnumerable<Alert>> Handle(GetAllAlertsByProductIdQuery query);
    Task<IEnumerable<Alert>> Handle(GetAllAlertsByAccountIdQuery query);
    Task<IEnumerable<Alert>> Handle(GetAllAlertsByWarehouseIdQuery query);
}