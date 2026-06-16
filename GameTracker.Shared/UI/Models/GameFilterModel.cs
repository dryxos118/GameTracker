using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.UI.Enums;

namespace GameTracker.Shared.UI.Models;

public sealed class GameFilterModel
{
    public string Search { get; set; } = string.Empty;
    public string PlatformName { get; set; } = string.Empty;
    public string? LauncherName { get; set; }
    public GamePlayStatus? GamePlayStatus { get; set; }
    public bool FavoritesOnly { get; set; }
    public GameSortBy SortBy { get; set; } = GameSortBy.Recent;

    public GameFilterModel Clone()
    {
        return new GameFilterModel
        {
            Search = Search,
            PlatformName = PlatformName,
            LauncherName = LauncherName,
            GamePlayStatus = GamePlayStatus,
            FavoritesOnly = FavoritesOnly,
            SortBy = SortBy
        };
    }
}