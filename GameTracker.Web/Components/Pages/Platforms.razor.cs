using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.State;
using Microsoft.AspNetCore.Components;

namespace GameTracker.Web.Components.Pages;

public partial class Platforms : AppComponentBase, IDisposable
{
    [Inject] private PlatformState PlatformState { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        PlatformState.OnChange += StateHasChanged;

        if (!PlatformState.IsLoaded)
            await PlatformState.LoadAsync();
    }

    public void Dispose()
    {
        PlatformState.OnChange -= StateHasChanged;
    }
}