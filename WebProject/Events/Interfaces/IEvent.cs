namespace WebProject.Events.Interfaces;

public interface IEvent
{
    DateTime EventDate { get; }
    string EventType { get; }
}
