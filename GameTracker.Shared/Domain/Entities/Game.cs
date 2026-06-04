using GameTracker.Shared.Domain.Enums;

namespace GameTracker.Shared.Domain.Entities;

public class Game
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? CoverUrl { get; set; }
    
    public GameLibraryStatus LibraryStatus { get; set; } = GameLibraryStatus.Library;
    public GamePlayStatus? PlayStatus { get; set; } = GamePlayStatus.Backlog;
    
    public GameMode Modes { get; set; } = GameMode.Solo;

    public bool IsFavorite { get; set; }

    public int? Rating { get; set; }
    public int PlayedHours { get; set; }

    public DateOnly? ReleaseDate { get; set; }
    
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public int PlatformId { get; set; }
    public Platform Platform { get; set; } = null!;

    public int? LauncherId { get; set; }
    public Launcher? Launcher { get; set; }

    public List<GameGenre> GameGenres { get; set; } = [];
    public List<GameTag> GameTags { get; set; } = [];
}