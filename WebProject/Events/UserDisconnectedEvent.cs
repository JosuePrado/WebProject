using WebProject.Events.Interfaces;

namespace WebProject.Events;

public class UserDisconnectedEvent : IEvent
{
    public string User { get; set; }
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public string EventType { get; } = nameof(UserConnectedEvent);

    public UserDisconnectedEvent(string user)
    {
        User = user;
    }
}
