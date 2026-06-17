using GameTracker.Shared.Domain.Entities;
using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Taxonomy.Dtos;
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

    public async Task<List<GenreListDto>> GetGenresAsync()
    {
        return await _ctx.Genres
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new GenreListDto(g.Id, g.Name, g.GameGenres.Count
            ))
            .ToListAsync();
    }

    public async Task<List<TagListDto>> GetTagsAsync()
    {
        return await _ctx.Tags
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .Select(t => new TagListDto(t.Id, t.Name, t.GameTags.Count))
            .ToListAsync();
    }

    public async Task<bool> AddAsync(TaxonomyReferenceType type, NameFormDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return false;

        string name = dto.Name.Trim();

        switch (type)
        {
            case TaxonomyReferenceType.Genre:
                _ctx.Genres.Add(new Genre { Name = name });
                break;

            case TaxonomyReferenceType.Tag:
                _ctx.Tags.Add(new Tag { Name = name });
                break;

            default:
                return false;
        }

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(TaxonomyReferenceType type, NameFormDto dto)
    {
        if (dto.Id is null || string.IsNullOrWhiteSpace(dto.Name))
            return false;

        string name = dto.Name.Trim();

        switch (type)
        {
            case TaxonomyReferenceType.Genre:
                Genre? genre = await _ctx.Genres.FindAsync(dto.Id.Value);

                if (genre is null)
                    return false;

                genre.Name = name;
                break;

            case TaxonomyReferenceType.Tag:
                Tag? tag = await _ctx.Tags.FindAsync(dto.Id.Value);

                if (tag is null)
                    return false;

                tag.Name = name;
                break;

            default:
                return false;
        }

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<OperationResult> DeleteAsync(TaxonomyReferenceType type, int id)
    {
        switch (type)
        {
            case TaxonomyReferenceType.Genre:
            {
                bool inUse = await _ctx.Games.AnyAsync(g => g.GameGenres.Any(gg => gg.GenreId == id));

                if (inUse)
                    return OperationResult.InUse;

                int affectedRows = await _ctx.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();

                return affectedRows > 0 ? OperationResult.Success : OperationResult.NotFound;
            }

            case TaxonomyReferenceType.Tag:
            {
                bool inUse = await _ctx.Games.AnyAsync(g => g.GameTags.Any(gg => gg.TagId == id));

                if (inUse)
                    return OperationResult.InUse;

                int affectedRows = await _ctx.Tags.Where(t => t.Id == id).ExecuteDeleteAsync();

                return affectedRows > 0 ? OperationResult.Success : OperationResult.NotFound;
            }

            default:
                return OperationResult.NotFound;
        }
    }
}