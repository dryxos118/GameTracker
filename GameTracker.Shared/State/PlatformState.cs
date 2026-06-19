using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Platforms;
using GameTracker.Shared.Features.Platforms.Dtos;

namespace GameTracker.Shared.State;

public sealed class PlatformState(PlatformService platformService)
{
    private readonly PlatformService _platformService = platformService;

    public bool IsLoading { get; private set; }

    public bool IsLoaded { get; private set; }

    public List<PlatformListDto> Platforms { get; private set; } = [];

    public List<LauncherListDto> Launchers { get; private set; } = [];

    public event Action? OnChange;

    public async Task LoadAsync()
    {
        IsLoading = true;
        IsLoaded = false;
        NotifyStateChanged();

        Platforms = await _platformService.GetPlatformsAsync();
        Launchers = await _platformService.GetLaunchersAsync();

        IsLoading = false;
        IsLoaded = true;
        NotifyStateChanged();
    }

    public async Task<OperationResult> AddAsync(PlatformReferenceType type, NameFormDto dto)
    {
        OperationResult result = await _platformService.AddAsync(type, dto);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    public async Task<OperationResult> UpdateAsync(PlatformReferenceType type, NameFormDto dto)
    {
        OperationResult result = await _platformService.UpdateAsync(type, dto);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    public async Task<OperationResult> DeleteAsync(PlatformReferenceType type, int id)
    {
        OperationResult result = await _platformService.DeleteAsync(type, id);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}