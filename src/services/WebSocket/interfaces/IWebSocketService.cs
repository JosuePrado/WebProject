using Fleck;

namespace WebProject.Services.WebSocket.Interfaces;

public interface IWebSocketService
{
    void Start();
    void AddClientToChannel(string channel, IWebSocketConnection wsConnection);
    void RemoveClientFromChannel(IWebSocketConnection wsConnection);
    void BroadcastMessageToChannel(string channel, string message, IWebSocketConnection sender);
}
