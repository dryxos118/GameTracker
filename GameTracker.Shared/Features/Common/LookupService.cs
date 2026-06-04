using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Common;

public sealed class LookupService(AppDbContext context)
{
    private readonly AppDbContext _ctx = context;

    public async Task<List<LookupItemDto>> GetPlatformsAsync()
    {
        return await _ctx.Platforms
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new LookupItemDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
    }

    public async Task<List<LookupItemDto>> GetLaunchersAsync()
    {
        return await _ctx.Launchers
            .AsNoTracking()
            .OrderBy(l => l.Name)
            .Select(l => new LookupItemDto
            {
                Id = l.Id,
                Name = l.Name
            }).ToListAsync();
    }

    public async Task<List<LookupItemDto>> GetGenresAsync()
    {
        return await _ctx.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new LookupItemDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToListAsync();
    }

    public async Task<List<LookupItemDto>> GetTagsAsync()
    {
        return await _ctx.Tags
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .Select(t => new LookupItemDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToListAsync();
    }
}