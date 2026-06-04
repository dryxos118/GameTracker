using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Dashboard.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Dashboard;

public sealed class DashBoardService(AppDbContext context)
{
    private readonly AppDbContext _ctx = context;

    public async Task<int> GetTotalGamesAsync()
    {
        return await _ctx.Games.AsNoTracking().CountAsync();
    }

    public async Task<DashboardStatsDto> GetStatsAsync()
    {
        int favoriteCount = await _ctx.Games.AsNoTracking()
            .CountAsync(game => game.IsFavorite);

        int backlogCount = await _ctx.Games.AsNoTracking()
            .CountAsync(game => game.PlayStatus == GamePlayStatus.Backlog);

        int playingCount = await _ctx.Games.AsNoTracking()
            .CountAsync(game => game.PlayStatus == GamePlayStatus.Playing);

        int completedCount = await _ctx.Games.AsNoTracking()
            .CountAsync(game => game.PlayStatus == GamePlayStatus.Completed);

        return new DashboardStatsDto(favoriteCount, backlogCount, playingCount, completedCount);
    }

    public async Task<List<ContinuePlayingGameDto>> GetContinuePlayingGamesAsync()
    {
        return await _ctx.Games.AsNoTracking()
            .Where(game => game.PlayStatus == GamePlayStatus.Playing || game.PlayStatus == GamePlayStatus.Paused)
            .OrderByDescending(game => game.UpdatedAt).Take(4)
            .Select(game => new ContinuePlayingGameDto(
                game.Id,
                game.Title,
                game.CoverUrl,
                game.Platform.Name,
                game.Launcher != null ? game.Launcher.Name : null,
                game.PlayedHours,
                game.Rating))
            .ToListAsync();
    }

    public async Task<List<RecentGameDto>> GetRecentGamesAsync()
    {
        return await _ctx.Games.AsNoTracking()
            .OrderByDescending(game => game.AddedAt).Take(6)
            .Select(game => new RecentGameDto(
                game.Id,
                game.Title,
                game.CoverUrl,
                game.Platform.Name,
                game.Rating
            ))
            .ToListAsync();
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        int wishlistCount = await _ctx.Games.AsNoTracking()
            .CountAsync(game => game.LibraryStatus == GameLibraryStatus.Wishlist);

        int totalPlayedHours = await _ctx.Games.AsNoTracking()
            .SumAsync(game => game.PlayedHours);

        var favoritePlatform = await _ctx.Games.AsNoTracking()
            .GroupBy(game => game.Platform.Name)
            .Select(group => new
            {
                Name = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(platform => platform.Count)
            .FirstOrDefaultAsync();

        int totalGames = await _ctx.Games.AsNoTracking().CountAsync();

        return new DashboardSummaryDto(
            wishlistCount,
            totalPlayedHours,
            favoritePlatform?.Name ?? "N/A",
            favoritePlatform?.Count ?? 0,
            totalGames
        );
    }
}