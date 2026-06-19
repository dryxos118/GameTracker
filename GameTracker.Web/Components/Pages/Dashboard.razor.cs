using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.State;
using Microsoft.AspNetCore.Components;

namespace GameTracker.Web.Components.Pages;

public partial class Dashboard : AppComponentBase, IDisposable
{
    [Inject] private DashboardState DashboardState { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        DashboardState.OnChange += StateHasChanged;
        await DashboardState.LoadAsync();
    }

    public void Dispose()
    {
        DashboardState.OnChange -= StateHasChanged;
    }
}