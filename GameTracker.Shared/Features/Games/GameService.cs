using GameTracker.Shared.Domain.Entities;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using GameTracker.Shared.State;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Games;

public sealed class GameService(AppDbContext context, DashboardState dashboardState, DatabaseState databaseState)
{
    private readonly AppDbContext _ctx = context;
    private readonly DashboardState _dashboardState = dashboardState;
    private readonly DatabaseState _databaseState = databaseState;

    public async Task<List<GameListItemDto>> GetLibraryAsync()
    {
        return await _ctx.Games
            .AsNoTracking()
            .Where(x => x.LibraryStatus == GameLibraryStatus.Library)
            .Select(x => new GameListItemDto
            {
                Id = x.Id,
                Title = x.Title,
                CoverUrl = x.CoverUrl,
                PlayStatus = x.PlayStatus,
                Modes = x.Modes,
                PlatformName = x.Platform.Name,
                LauncherName = x.Launcher != null ? x.Launcher.Name : null,
                IsFavorite = x.IsFavorite,
                PlayedHours = x.PlayedHours,
                Rating = x.Rating,
                AddedAt = x.AddedAt
            })
            .ToListAsync();
    }

    public async Task<GameFormDto> GetGameFormAsync(int gameId)
    {
        Game? game = await _ctx.Games.AsNoTracking()
            .Include(x => x.GameGenres)
            .Include(x => x.GameTags)
            .FirstOrDefaultAsync(x => x.Id == gameId);

        return game is null
            ? new GameFormDto()
            : game.ToDto();
    }

    public async Task<int> AddAsync(GameFormDto dto)
    {
        Game game = dto.ToEntity();

        _ctx.Games.Add(game);

        await _ctx.SaveChangesAsync();

        _dashboardState.NotifyStateChanged();
        _databaseState.NotifyDatabaseChanged();

        return game.Id;
    }

    public async Task<bool> UpdateAsync(GameFormDto dto)
    {
        if (dto.Id is null)
            return false;

        Game? game = await _ctx.Games
            .Include(g => g.GameGenres)
            .Include(g => g.GameTags)
            .FirstOrDefaultAsync(g => g.Id == dto.Id.Value);

        if (game is null)
            return false;

        Game updatedGame = dto.ToEntity();

        _ctx.Entry(game).CurrentValues.SetValues(updatedGame);

        game.GameGenres.Clear();
        game.GameTags.Clear();

        game.GameGenres.AddRange(updatedGame.GameGenres);
        game.GameTags.AddRange(updatedGame.GameTags);

        await _ctx.SaveChangesAsync();

        _dashboardState.NotifyStateChanged();
        _databaseState.NotifyDatabaseChanged();

        return true;
    }

    public async Task<bool> DeleteAsync(int gameId)
    {
        int affectedRows = await _ctx.Games.Where(g => g.Id == gameId).ExecuteDeleteAsync();

        _dashboardState.NotifyStateChanged();
        _databaseState.NotifyDatabaseChanged();

        return affectedRows > 0;
    }
}