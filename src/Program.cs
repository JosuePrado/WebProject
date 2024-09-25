using Fleck;

var server = new WebSocketServer("ws://127.0.0.1:8181");

var wsClientsConnected = new List<IWebSocketConnection>();

server.Start(wsConnection =>
{
    wsConnection.OnOpen = () =>
    {
        wsClientsConnected.Add(wsConnection);
    };

    wsConnection.OnMessage = message =>
    {
        wsClientsConnected.ForEach(client =>
        {
            if (client != wsConnection)
            {
                client.Send(message);
            }

        });
    };
});

WebApplication.CreateBuilder(args).Build().Run();
