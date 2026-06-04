using GameTracker.Shared.Features.Common;
using GameTracker.Shared.Features.Dashboard;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Settings;
using GameTracker.Shared.State;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGameTrackerSharedServices(this IServiceCollection services)
    {
        services.AddScoped<DashBoardService>();
        services.AddScoped<GameService>();
        services.AddScoped<SettingsService>();
        services.AddScoped<LookupService>();

        services.AddScoped<DatabaseState>();
        services.AddScoped<DashboardState>();
        services.AddScoped<GameState>();
    } 
}