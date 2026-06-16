using GameTracker.Shared.Features.Platforms;
using GameTracker.Shared.Features.Platforms.Dtos;

namespace GameTracker.Shared.State;

public sealed class PlatformState(PlatformService platformService)
{
    private readonly PlatformService _platformService = platformService;
    
    public bool IsLoading { get; private set; }
    
    public bool IsLoaded { get; private set; }

    public List<PlatformListDto> Platforms { get; private set; } = [];
    
    public List<LauncherListDto>  Launchers { get; private set; } = [];
    
    public event Action? OnChange;

    public async Task LoadAsync()
    {
        IsLoading = true;
        NotifyStateChanged();
        
        Platforms = await _platformService.GetPlatformsAsync();
        Launchers = await _platformService.GetLaunchersAsync();
        
        IsLoading = false;
        IsLoaded = true;
        NotifyStateChanged();
    }

    public async Task ReloadAsync()
    {
        IsLoaded = false;
        await LoadAsync();
    }
    
    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}