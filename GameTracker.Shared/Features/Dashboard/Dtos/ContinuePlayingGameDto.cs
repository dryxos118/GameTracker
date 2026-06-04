namespace GameTracker.Shared.Features.Dashboard.Dtos;

public sealed record ContinuePlayingGameDto(
    int Id,
    string Title,
    string? CoverUrl,
    string Platform,
    string? Launcher,
    int PlayedHours,
    int? Rating
);