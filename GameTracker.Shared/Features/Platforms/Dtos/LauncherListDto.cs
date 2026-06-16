namespace GameTracker.Shared.Features.Platforms.Dtos;

public sealed record LauncherListDto(
    int Id,
    string Name,
    int TotalGames
);