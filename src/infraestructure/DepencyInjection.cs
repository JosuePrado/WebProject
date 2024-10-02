using WebProject.Data;
using WebProject.Data.Concretes;
using WebProject.Data.Interfaces;
using WebProject.Services;
using WebProject.Services.WebSocket;
using WebProject.Services.WebSocket.Interfaces;

namespace WebProject.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddServices(configuration);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.ConnectionStrings));
        services.AddScoped<IDbConnectionSingleton, DbConnection>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WebSocketConfiguration>(configuration.GetSection(WebSocketConfiguration.WebSocketUrl));
        services.AddScoped<IWebSocketService, WebSocketService>();
        return services;
    }
}
