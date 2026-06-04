namespace GameTracker.Shared.Infrastructure.Seed;

public sealed record SeedResult(
    bool HasSeeded,
    int Platforms,
    int Launchers,
    int Genres,
    int Tags,
    int Games);