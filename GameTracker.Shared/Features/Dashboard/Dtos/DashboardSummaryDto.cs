namespace GameTracker.Shared.Features.Dashboard.Dtos;

public sealed record DashboardSummaryDto(
    int WishlistCount,
    int TotalPlayedHours,
    string FavoritePlatform,
    int FavoritePlatformCount,
    int TotalGames
);