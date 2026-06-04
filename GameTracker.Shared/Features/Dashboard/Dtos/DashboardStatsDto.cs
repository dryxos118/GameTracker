namespace GameTracker.Shared.Features.Dashboard.Dtos;

public sealed record DashboardStatsDto(
    int FavoriteCount,
    int BacklogCount,
    int PlayingCount,
    int CompletedCount
);