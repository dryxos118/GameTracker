namespace GameTracker.Shared.Features.Taxonomy.Dtos;

public sealed record TagListDto(
    int Id,
    string Name,
    int TotalGames
);