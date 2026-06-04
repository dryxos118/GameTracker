using GameTracker.Shared.Infrastructure.Seed;

namespace GameTracker.Shared.Features.Settings.Dtos;

public sealed record ResetDatabaseResult(
    bool SeedDemoData,
    SeedResult? SeedResult);