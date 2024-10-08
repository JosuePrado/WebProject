using Microsoft.AspNetCore.SignalR;
using WebProject.Data;
using WebProject.Events;
using WebProject.Events.Interfaces;

namespace WebProject.Hubs;

public class ChatHub : Hub
{
    private readonly IEventBus _eventBus;

    public ChatHub(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task SendMessage(string message, int channelId)
    {
        string user = UserContext.CurrentUserName;
        var newMessageEvent = new NewMessageEvent(message, user, channelId);
        _eventBus.Publish(newMessageEvent);
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        string user = UserContext.CurrentUserName;
        var userConnectedEvent = new UserConnectedEvent(user);
        _eventBus.Publish(userConnectedEvent);

        await Clients.All.SendAsync("UserConnected", user);
        await base.OnConnectedAsync();
    }

    public async Task UserTyping()
    {
        await Clients.Others.SendAsync("ReceiveTypingNotification", UserContext.CurrentUserName);
    }
}