using WebProject.Data;
using WebProject.Data.Concretes;
using WebProject.Data.Interfaces;
using WebProject.EventHandlers;
using WebProject.EventHandlers.Interfaces;
using WebProject.Events;
using WebProject.Events.Interfaces;
using WebProject.Repositories;
using WebProject.Repositories.Interfaces;

namespace WebProject.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddEvents()
            .AddRepositories();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.ConnectionStrings));
        services.AddSingleton<IDbConnectionSingleton, DbConnection>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        return services;
    }

    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, EventBus>();
        services.AddTransient<NewMessageEventHandler>();
        services.AddTransient<UserConnectedEventHandler>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChannelRepository, ChannelRepository>();
        return services;
    }
}
