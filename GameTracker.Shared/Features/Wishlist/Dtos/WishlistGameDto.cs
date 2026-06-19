namespace GameTracker.Shared.Features.Wishlist.Dtos;

public sealed class WishlistGameDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? CoverUrl { get; set; }

    public string PlatformName { get; set; } = string.Empty;

    public string? LauncherName { get; set; }
    
    public DateTime AddedAt { get; set; }
}