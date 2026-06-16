using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Taxonomy;

public sealed class TaxonomyService(AppDbContext context)
{
    private readonly AppDbContext _ctx = context;

    public async Task<List<SelectItemDto<int>>> GetGenreSelectItemsAsync()
    {
        return await _ctx.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new SelectItemDto<int>(g.Name, g.Id))
            .ToListAsync();
    }

    public async Task<List<SelectItemDto<int>>> GetTagSelectItemsAsync()
    {
        return await _ctx.Tags
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .Select(t => new SelectItemDto<int>(t.Name, t.Id))
            .ToListAsync();
    }
}