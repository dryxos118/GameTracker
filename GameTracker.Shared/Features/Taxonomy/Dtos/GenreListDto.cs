namespace GameTracker.Shared.Features.Taxonomy.Dtos;

public sealed record GenreListDto(
    int Id,
    string Name,
    int TotalGames
);