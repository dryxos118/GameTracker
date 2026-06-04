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
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory = dbContextFactory;
    private readonly DatabaseState _databaseState = databaseState;
    private readonly DashboardState _dashboardState = dashboardState;

    public async Task<SeedResult> SeedDemoDataAsync()
    {
        await using AppDbContext context =
            await _dbContextFactory.CreateDbContextAsync();

        SeedResult result = await DatabaseSeeder.SeedAsync(context);

        if (result.HasSeeded)
        {
            _databaseState.NotifyDatabaseChanged();
            _dashboardState.Clear();
        }

        return result;
    }

    public async Task<ResetDatabaseResult> ResetDatabaseAsync(bool seedDemoData)
    {
        await using AppDbContext context =
            await _dbContextFactory.CreateDbContextAsync();

        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();

        SeedResult? seedResult = null;

        if (seedDemoData)
        {
            seedResult = await DatabaseSeeder.SeedAsync(context);
        }

        context.ChangeTracker.Clear();

        _databaseState.NotifyDatabaseChanged();
        _dashboardState.Clear();

        return new ResetDatabaseResult(
            SeedDemoData: seedDemoData,
            SeedResult: seedResult);
    }
}