using WebProject.Events;

namespace WebProject.EventHandlers.Interfaces;

public interface IEventHandler<T>
{
    void Handle(T @event);
}