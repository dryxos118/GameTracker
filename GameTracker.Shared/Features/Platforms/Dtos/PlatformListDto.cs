namespace GameTracker.Shared.Features.Platforms.Dtos;

public sealed record PlatformListDto(
    int Id,
    string Name,
    int TotalGames
);