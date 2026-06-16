using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.UI.Enums;

namespace GameTracker.Shared.Features.Games.Dtos;

public sealed class GameFormDto
{
    public int? Id { get; set; }

    // INFO
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string? CoverUrl { get; set; }

    // UI
    public GameCoverSource CoverSource { get; set; } = GameCoverSource.Url;

    public int? SteamAppId { get; set; }

    //
    public DateOnly? ReleaseDate { get; set; }

    // Library
    public GameLibraryStatus LibraryStatus { get; set; } = GameLibraryStatus.Library;
    public GamePlayStatus? PlayStatus { get; set; } = GamePlayStatus.Playing;
    public bool IsFavorite { get; set; } = false;
    public int Rating { get; set; } = 0;
    public int PlayedHours { get; set; } = 0;

    // Classification
    public int? PlatformId { get; set; }
    public int? LauncherId { get; set; }
    public GameMode Modes { get; set; } = GameMode.Solo;
    public List<int> GenreIds { get; set; } = [];
    public List<int> TagIds { get; set; } = [];
}