using GameTracker.Shared.Extensions;
using GameTracker.Web.App.Services;
using GameTracker.Web.App.State;

namespace GameTracker.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGameTrackerServices(this IServiceCollection services)
    {
        services.AddGameTrackerSharedServices();

        services.AddScoped<ThemeState>();
        services.AddScoped<ThemePersistenceService>();
    }
}