using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Wishlist;
using GameTracker.Shared.Features.Wishlist.Dtos;
using GameTracker.Shared.UI.Enums;
using GameTracker.Shared.UI.Models;

namespace GameTracker.Shared.State;

public sealed class WishlistState(WishlistService wishlistService)
{
    private readonly WishlistService _wishlistService = wishlistService;

    public bool IsLoading { get; private set; }

    public bool IsLoaded { get; private set; }

    public List<WishlistGameDto> WishlistGames { get; private set; } = [];

    public WishlistFilterModel Filters { get; private set; } = new();

    public bool HasActiveFilters =>
        !string.IsNullOrWhiteSpace(Filters.Search) ||
        !string.IsNullOrWhiteSpace(Filters.PlatformName) ||
        !string.IsNullOrWhiteSpace(Filters.LauncherName) ||
        Filters.SortBy != GameSortBy.Recent;

    public IReadOnlyList<WishlistGameDto> FilteredGames
    {
        get
        {
            IEnumerable<WishlistGameDto> query = WishlistGames;

            if (!string.IsNullOrWhiteSpace(Filters.Search))
                query = query
                    .Where(x => x.Title.Contains(Filters.Search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(Filters.PlatformName))
                query = query.Where(x => x.PlatformName == Filters.PlatformName);

            if (!string.IsNullOrWhiteSpace(Filters.LauncherName))
                query = query.Where(x => x.LauncherName == Filters.LauncherName);

            query = Filters.SortBy switch
            {
                GameSortBy.Title => query.OrderBy(x => x.Title),
                _ => query.OrderByDescending(x => x.AddedAt)
            };

            return query.ToList();
        }
    }

    public event Action? OnChange;

    public async Task LoadAsync()
    {
        IsLoading = false;
        IsLoading = true;
        NotifyStateChanged();

        WishlistGames = await _wishlistService.GetWishlistAsync();

        IsLoading = false;
        IsLoaded = true;
        NotifyStateChanged();
    }

    public async Task<OperationResult> MoveToLibraryAsync(int id)
    {
        OperationResult result = await _wishlistService.MoveToLibraryAsync(id);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    public void ApplyFilters(WishlistFilterModel filters)
    {
        Filters = filters;
        NotifyStateChanged();
    }

    public void ResetFilters()
    {
        Filters.Search = string.Empty;
        Filters.PlatformName = string.Empty;
        Filters.LauncherName = null;
        Filters.SortBy = GameSortBy.Recent;
        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}