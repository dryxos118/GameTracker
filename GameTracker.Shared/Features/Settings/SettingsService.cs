using GameTracker.Shared.Features.Settings.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using GameTracker.Shared.Infrastructure.Seed;
using GameTracker.Shared.State;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Settings;

public sealed class SettingsService(
    IDbContextFactory<AppDbContext> dbContextFactory,
    DatabaseState databaseState,
    DashboardState dashboardState
)
{
    private readonly DashboardState _dashboardState = dashboardState;
    private readonly DatabaseState _databaseState = databaseState;
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory = dbContextFactory;

    public async Task<SeedResult> SeedDemoDataAsync()
    {
        await using AppDbContext context =
            await _dbContextFactory.CreateDbContextAsync();

        SeedResult result = await DatabaseSeeder.SeedAsync(context);

        if (!result.HasSeeded) return result;
        
        _databaseState.NotifyDatabaseChanged();
        _dashboardState.Clear();

        return result;
    }

    public async Task<ResetDatabaseResult> ResetDatabaseAsync(bool seedDemoData)
    {
        await using AppDbContext context =
            await _dbContextFactory.CreateDbContextAsync();

        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();

        SeedResult? seedResult = null;

        if (seedDemoData) seedResult = await DatabaseSeeder.SeedAsync(context);

        context.ChangeTracker.Clear();

        _databaseState.NotifyDatabaseChanged();
        _dashboardState.Clear();

        return new ResetDatabaseResult(
            seedDemoData,
            seedResult);
    }
}