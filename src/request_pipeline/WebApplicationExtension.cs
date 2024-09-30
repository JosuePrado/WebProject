namespace WebProject.RequestPipeline;

using WebProject.Data.Interfaces;
using WebProject.Services.WebSocket.Interfaces;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            dbInitializer.InitializeDatabase();

            return app;
        }
    }

    public static WebApplication InitializeWebSocket(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var webSocketService = app.Services.GetRequiredService<IWebSocketService>();
            
            webSocketService.Start();

            return app;
        }
    }
}