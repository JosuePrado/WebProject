using WebProject.Events.Interfaces;

namespace WebProject.Events;

public class UserConnectedEvent : IEvent
{
    public string Username { get; set; }
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public string EventType { get; } = nameof(UserConnectedEvent);

    public UserConnectedEvent(string username)
    {
        Username = username;
    }
}
