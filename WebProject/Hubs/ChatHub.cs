using Microsoft.AspNetCore.SignalR;
using WebProject.Data;
using WebProject.Events;
using WebProject.Events.Interfaces;

namespace WebProject.Hubs;

public class ChatHub : Hub
{
    private readonly IEventBus _eventBus;

    private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

    public ChatHub(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task SendMessage(string message, int channelId)
    {
        string user = _connections[Context.ConnectionId];
        var newMessageEvent = new NewMessageEvent(message, user, channelId);
        _eventBus.Publish(newMessageEvent);
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        string user = UserContext.CurrentUserName;
        _connections[Context.ConnectionId] = user;
        var userConnectedEvent = new UserConnectedEvent(user);
        _eventBus.Publish(userConnectedEvent);

        await Clients.All.SendAsync("UserConnected", user);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out string? user))
        {
            _connections.Remove(Context.ConnectionId);
            var userDisconnectedEvent = new UserDisconnectedEvent(user);
            _eventBus.Publish(userDisconnectedEvent);

            await Clients.All.SendAsync("UserDisconnected", user);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task UserTyping()
    {
        await Clients.Others.SendAsync("ReceiveTypingNotification", _connections[Context.ConnectionId]);
    }
}
