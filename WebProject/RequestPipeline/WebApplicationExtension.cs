namespace WebProject.RequestPipeline;

using WebProject.Data.Interfaces;
using WebProject.EventHandlers;
using WebProject.Events;
using WebProject.Events.Interfaces;

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

    public static WebApplication InitializeEventHandlers(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
            var messageHandler = scope.ServiceProvider.GetRequiredService<NewMessageEventHandler>();

            eventBus.Subscribe<NewMessageEvent>(messageHandler.Handle);

            return app;
        }
    }
}
