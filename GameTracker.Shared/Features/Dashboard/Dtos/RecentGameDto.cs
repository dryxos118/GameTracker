namespace GameTracker.Shared.Features.Dashboard.Dtos;

public sealed record RecentGameDto(
    int Id,
    string Title,
    string? CoverUrl,
    string Platform
);