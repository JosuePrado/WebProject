using Fleck;
using System;
using System.Collections.Generic;

var server = new WebSocketServer("ws://127.0.0.1:8181");

var channelClients = new Dictionary<string, List<IWebSocketConnection>>();

server.Start(wsConnection =>
{
    wsConnection.OnOpen = () =>
    {
        var query = wsConnection.ConnectionInfo.Path;
        var channel = query.TrimStart('?');

        if (string.IsNullOrEmpty(channel))
        {
            channel = "default";
        }

        if (!channelClients.ContainsKey(channel))
        {
            channelClients[channel] = new List<IWebSocketConnection>();
        }
        channelClients[channel].Add(wsConnection);

        Console.WriteLine($"Cliente conectado al canal: {channel}");
    };

    wsConnection.OnClose = () =>
    {
        foreach (var channel in channelClients.Keys)
        {
            if (channelClients[channel].Contains(wsConnection))
            {
                channelClients[channel].Remove(wsConnection);
                Console.WriteLine($"Cliente desconectado del canal: {channel}");
                break;
            }
        }
    };

    wsConnection.OnMessage = message =>
    {
        var query = wsConnection.ConnectionInfo.Path;
        var channel = query.TrimStart('?');

        if (string.IsNullOrEmpty(channel))
        {
            channel = "default";
        }

        if (channelClients.ContainsKey(channel))
        {
            channelClients[channel].ForEach(client =>
            {
                if (client != wsConnection)
                {
                    client.Send(message);
                }
            });
        }
    };
});

WebApplication.CreateBuilder(args).Build().Run();
