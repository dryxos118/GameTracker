using GameTracker.Shared.Domain.Entities;
using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Platforms.Dtos;
using GameTracker.Shared.Infrastructure.Persistence;
using GameTracker.Shared.State;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Features.Platforms;

public sealed class PlatformService(AppDbContext context)
{
    private readonly AppDbContext _ctx = context;

    public async Task<List<SelectItemDto<int>>> GetPlatformSelectItemsAsync()
    {
        return await _ctx.Platforms.AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new SelectItemDto<int>(p.Name, p.Id))
            .ToListAsync();
    }

    public async Task<List<SelectItemDto<int>>> GetLauncherSelectItemsAsync()
    {
        return await _ctx.Launchers.AsNoTracking()
            .OrderBy(l => l.Name)
            .Select(l => new SelectItemDto<int>(l.Name, l.Id))
            .ToListAsync();
    }

    public async Task<List<PlatformListDto>> GetPlatformsAsync()
    {
        return await _ctx.Platforms.AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new PlatformListDto(
                p.Id,
                p.Name,
                p.Games.Count
            ))
            .ToListAsync();
    }

    public async Task<List<LauncherListDto>> GetLaunchersAsync()
    {
        return await _ctx.Launchers.AsNoTracking()
            .OrderBy(l => l.Name)
            .Select(l => new LauncherListDto(
                l.Id,
                l.Name,
                l.Games.Count
            ))
            .ToListAsync();
    }

    public async Task<OperationResult> AddAsync(PlatformReferenceType type, NameFormDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return OperationResult.Error;

        string name = dto.Name.Trim();

        switch (type)
        {
            case PlatformReferenceType.Platform:
                _ctx.Platforms.Add(new Platform { Name = name });
                break;

            case PlatformReferenceType.Launcher:
                _ctx.Launchers.Add(new Launcher { Name = name });
                break;

            default:
                return OperationResult.Error;
        }

        int affectedRows = await _ctx.SaveChangesAsync();
        return affectedRows > 0 ? OperationResult.Success : OperationResult.Error;
    }

    public async Task<OperationResult> UpdateAsync(PlatformReferenceType type, NameFormDto dto)
    {
        if (dto.Id is null || string.IsNullOrWhiteSpace(dto.Name))
            return OperationResult.Error;

        string name = dto.Name.Trim();

        if (dto.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            return OperationResult.Success;

        switch (type)
        {
            case PlatformReferenceType.Platform:
                Platform? platform = await _ctx.Platforms.FindAsync(dto.Id.Value);

                if (platform is null)
                    return OperationResult.NotFound;

                platform.Name = name;
                break;

            case PlatformReferenceType.Launcher:
                Launcher? launcher = await _ctx.Launchers.FindAsync(dto.Id.Value);

                if (launcher is null)
                    return OperationResult.NotFound;

                launcher.Name = name;
                break;

            default:
                return OperationResult.Error;
        }

        int affectedRows = await _ctx.SaveChangesAsync();
        return affectedRows > 0 ? OperationResult.Success : OperationResult.Error;
    }

    public async Task<OperationResult> DeleteAsync(PlatformReferenceType type, int id)
    {
        switch (type)
        {
            case PlatformReferenceType.Platform:
            {
                bool inUse = await _ctx.Games.AnyAsync(g => g.PlatformId == id);

                if (inUse)
                    return OperationResult.InUse;

                int affectedRows = await _ctx.Platforms.Where(p => p.Id == id).ExecuteDeleteAsync();

                return affectedRows > 0 ? OperationResult.Success : OperationResult.NotFound;
            }

            case PlatformReferenceType.Launcher:
            {
                bool inUse = await _ctx.Games.AnyAsync(g => g.LauncherId == id);

                if (inUse)
                    return OperationResult.InUse;

                int affectedRows = await _ctx.Launchers.Where(l => l.Id == id).ExecuteDeleteAsync();

                return affectedRows > 0 ? OperationResult.Success : OperationResult.NotFound;
            }

            default:
                return OperationResult.Error;
        }
    }
}