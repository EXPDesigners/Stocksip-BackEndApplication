using Cortex.Mediator.Notifications;
using StockSip.Platform.API.Shared.Domain.Model.Events;

namespace StockSip.Platform.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}