namespace WebProject.Services;

using Fleck;
using Microsoft.Extensions.Options;
using WebProject.Services.WebSocket;
using WebProject.Services.WebSocket.Interfaces;

public class WebSocketService : IWebSocketService
{
    private WebSocketServer _server;
    private Dictionary<string, List<IWebSocketConnection>> _channelClients;
    private WebSocketConfiguration _webSocketConfiguration;

    public WebSocketService(IOptions<WebSocketConfiguration> webSocketConfiguration)
    {
        _webSocketConfiguration = webSocketConfiguration.Value;
        _server = new WebSocketServer(_webSocketConfiguration.WebSocketConnection);
        _channelClients = new Dictionary<string, List<IWebSocketConnection>>();
    }

    public void Start()
    {
        _server.Start(wsConnection =>
        {
            wsConnection.OnOpen = () => HandleOpenConnection(wsConnection);
            wsConnection.OnClose = () => RemoveClientFromChannel(wsConnection);
            wsConnection.OnMessage = message => HandleMessage(wsConnection, message);
        });
    }

    public void AddClientToChannel(string channel, IWebSocketConnection wsConnection)
    {
        if (!_channelClients.ContainsKey(channel))
        {
            _channelClients[channel] = new List<IWebSocketConnection>();
        }
        _channelClients[channel].Add(wsConnection);
        Console.WriteLine($"Cliente conectado al canal: {channel}");
    }

    public void RemoveClientFromChannel(IWebSocketConnection wsConnection)
    {
        foreach (var channel in _channelClients.Keys)
        {
            if (_channelClients[channel].Contains(wsConnection))
            {
                _channelClients[channel].Remove(wsConnection);
                Console.WriteLine($"Cliente desconectado del canal: {channel}");
                break;
            }
        }
    }

    public void BroadcastMessageToChannel(string channel, string message, IWebSocketConnection sender)
    {
        if (_channelClients.ContainsKey(channel))
        {
            _channelClients[channel].ForEach(client =>
            {
                if (client != sender)
                {
                    client.Send(message);
                }
            });
        }
    }

    private void HandleOpenConnection(IWebSocketConnection wsConnection)
    {
        var query = wsConnection.ConnectionInfo.Path;
        var channel = query.TrimStart('?');

        if (string.IsNullOrEmpty(channel))
        {
            channel = "default";
        }

        AddClientToChannel(channel, wsConnection);
    }

    private void HandleMessage(IWebSocketConnection wsConnection, string message)
    {
        var query = wsConnection.ConnectionInfo.Path;
        var channel = query.TrimStart('?');

        if (string.IsNullOrEmpty(channel))
        {
            channel = "default";
        }

        BroadcastMessageToChannel(channel, message, wsConnection);
    }
}
