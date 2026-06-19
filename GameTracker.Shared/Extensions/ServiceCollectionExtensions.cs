using GameTracker.Shared.Features.Dashboard;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Platforms;
using GameTracker.Shared.Features.Settings;
using GameTracker.Shared.Features.Taxonomy;
using GameTracker.Shared.Features.Wishlist;
using GameTracker.Shared.State;
using Microsoft.Extensions.DependencyInjection;

namespace GameTracker.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGameTrackerSharedServices(this IServiceCollection services)
    {
        // Dashboard
        services.AddScoped<DashBoardService>();
        services.AddScoped<DashboardState>();

        // Games
        services.AddScoped<GameService>();
        services.AddScoped<GameState>();

        // Wishlist
        services.AddScoped<WishlistService>();
        services.AddScoped<WishlistState>();

        // Platforms
        services.AddScoped<PlatformService>();
        services.AddScoped<PlatformState>();

        // Taxonomies
        services.AddScoped<TaxonomyService>();
        services.AddScoped<TaxonomyState>();

        // Settings
        services.AddScoped<SettingsService>();

        // Database
        services.AddScoped<DatabaseState>();
    }
}