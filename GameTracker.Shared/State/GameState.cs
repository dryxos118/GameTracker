using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.UI.Enums;
using GameTracker.Shared.UI.Models;

namespace GameTracker.Shared.State;

public sealed class GameState(GameService gameService)
{
    private readonly GameService _gameService = gameService;

    public bool IsLoading { get; private set; }
    
    public bool IsLoaded { get; private set; }

    public List<GameListItemDto> Games { get; private set; } = [];

    public GameFilterModel Filters { get; private set; } = new();

    public bool HasActiveFilters =>
        !string.IsNullOrWhiteSpace(Filters.Search)
        || !string.IsNullOrWhiteSpace(Filters.PlatformName)
        || !string.IsNullOrWhiteSpace(Filters.LauncherName)
        || Filters.GamePlayStatus is not null
        || Filters.FavoritesOnly
        || Filters.SortBy != GameSortBy.Recent;

    public IReadOnlyList<GameListItemDto> FilteredGames
    {
        get
        {
            IEnumerable<GameListItemDto> query = Games;

            if (!string.IsNullOrWhiteSpace(Filters.Search))
                query = query
                    .Where(x => x.Title.Contains(Filters.Search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(Filters.PlatformName))
                query = query.Where(x => x.PlatformName == Filters.PlatformName);

            if (!string.IsNullOrWhiteSpace(Filters.LauncherName))
                query = query.Where(x => x.LauncherName == Filters.LauncherName);

            if (Filters.GamePlayStatus is not null)
                query = query.Where(x => x.PlayStatus == Filters.GamePlayStatus);

            if (Filters.FavoritesOnly)
                query = query.Where(x => x.IsFavorite);

            query = Filters.SortBy switch
            {
                GameSortBy.Title => query.OrderBy(x => x.Title),
                GameSortBy.Rating => query.OrderByDescending(x => x.Rating),
                GameSortBy.Hours => query.OrderByDescending(x => x.PlayedHours),
                _ => query.OrderByDescending(x => x.AddedAt)
            };

            return query.ToList();
        }
    }

    public event Action? OnChange;

    public async Task LoadAsync()
    {
        IsLoading = true;
        NotifyStateChanged();

        Games = await _gameService.GetLibraryAsync();

        IsLoading = false;
        IsLoaded = true;
        NotifyStateChanged();
    }

    public async Task RefreshAsync()
    {
        IsLoaded = false;
        await LoadAsync();
    }

    public void ApplyFilters(GameFilterModel filters)
    {
        Filters = filters;
        NotifyStateChanged();
    }

    public void ResetFilters()
    {
        Filters.Search = string.Empty;
        Filters.PlatformName = string.Empty;
        Filters.LauncherName = null;
        Filters.GamePlayStatus = null;
        Filters.FavoritesOnly = false;
        Filters.SortBy = GameSortBy.Recent;
        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}