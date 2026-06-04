using GameTracker.Shared.Domain.Enums;

namespace GameTracker.Shared.Features.Games.Dtos;

public class GameListItemDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? CoverUrl { get; set; }

    public GamePlayStatus? PlayStatus { get; set; }
    public GameMode Modes { get; set; }

    public string PlatformName { get; set; } = string.Empty;
    public string? LauncherName { get; set; }

    public bool IsFavorite { get; set; }

    public int PlayedHours { get; set; }
    public int? Rating { get; set; }
    
    public DateTime AddedAt { get; set; }
}