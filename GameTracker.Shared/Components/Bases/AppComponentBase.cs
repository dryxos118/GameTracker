using GameTracker.Shared.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace GameTracker.Shared.Components.Bases;

public class AppComponentBase : ComponentBase
{
    protected bool IsInitialized;
    [Inject] public IStringLocalizer<AppLocalization> L { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) IsInitialized = true;

        await base.OnAfterRenderAsync(firstRender);
    }
}