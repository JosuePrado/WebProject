using Microsoft.AspNetCore.SignalR;
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

    public async Task SendMessage(string user, string message, int channelId)
    {
        var newMessageEvent = new NewMessageEvent(message, user, channelId);
        _eventBus.Publish(newMessageEvent);
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var userConnectedEvent = new UserConnectedEvent(Context.User.Identity.Name);
        _eventBus.Publish(userConnectedEvent);

        await Clients.All.SendAsync("UserConnected", Context.User.Identity.Name);
        await base.OnConnectedAsync();
    }

    public async Task UserTyping(string user)
    {
        await Clients.Others.SendAsync("ReceiveTypingNotification", user);
    }
}