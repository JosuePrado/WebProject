using WebProject.Events.Interfaces;

namespace WebProject.Events;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Action<IEvent>>> _suscribers = new();
    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var eventType = @event.GetType();
        if (_suscribers.ContainsKey(eventType))
        {
            foreach (var handler in _suscribers[eventType])
            {
                handler(@event);
            }
        }
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (!_suscribers.ContainsKey(eventType))
        {
            _suscribers[eventType] = new List<Action<IEvent>>();
        }
        _suscribers[eventType].Add(e => handler((TEvent) e));
    }
}
