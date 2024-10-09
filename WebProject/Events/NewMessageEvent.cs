using WebProject.Events.Interfaces;

namespace WebProject.Events;

public class NewMessageEvent : IEvent
{
    public string Content { get; set; }
    public string Email { get; set; }
    public int ChannelId { get; set; }
    public DateTime EventDate { get; } = DateTime.UtcNow;
    public string EventType { get; } = nameof(NewMessageEvent);

    public NewMessageEvent(string message, string email, int channelId)
    {
        Content = message;
        Email = email;
        ChannelId = channelId;
    }
}
