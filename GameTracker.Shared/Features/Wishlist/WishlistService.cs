using GameTracker.Shared.Domain.Entities;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Wishlist.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Wishlist;

public sealed class WishlistService(AppDbContext context)
{
    private readonly AppDbContext _ctx = context;

    public async Task<List<WishlistGameDto>> GetWishlistAsync()
    {
        return await _ctx.Games.AsNoTracking()
            .Where(g => g.LibraryStatus == GameLibraryStatus.Wishlist)
            .Select(g => new WishlistGameDto
            {
                Id = g.Id,
                Title = g.Title,
                CoverUrl = g.CoverUrl,
                PlatformName = g.Platform.Name,
                LauncherName = g.Launcher != null ? g.Launcher.Name : null,
                AddedAt = g.AddedAt,
            })
            .ToListAsync();
    }

    public async Task<OperationResult> MoveToLibraryAsync(int id)
    {
        Game? game = await _ctx.Games.FindAsync(id);

        if (game is null)
            return OperationResult.NotFound;

        game.LibraryStatus = GameLibraryStatus.Library;

        await _ctx.SaveChangesAsync();

        return OperationResult.Success;
    }
}