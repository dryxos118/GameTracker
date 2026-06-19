using GameTracker.Shared.UI.Enums;

namespace GameTracker.Shared.UI.Models;

public sealed class WishlistFilterModel
{
    public string Search { get; set; } = string.Empty;
    public string PlatformName { get; set; } = string.Empty;
    public string? LauncherName { get; set; }
    public GameSortBy SortBy { get; set; } = GameSortBy.Recent;

    public WishlistFilterModel Clone()
    {
        return new WishlistFilterModel
        {
            Search = Search,
            PlatformName = PlatformName,
            LauncherName = LauncherName,
            SortBy = SortBy,
        };
    }
}