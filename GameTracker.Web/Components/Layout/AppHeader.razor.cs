using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Features.Dashboard;
using GameTracker.Shared.State;
using Microsoft.AspNetCore.Components;

namespace GameTracker.Web.Components.Layout;

public partial class AppHeader : AppComponentBase, IDisposable
{
    [Parameter] public bool IsDarkMode { get; set; }
    [Parameter] public int Elevation { get; set; } = 1;
    [Parameter] public EventCallback OnDrawerToggle { get; set; }
    [Parameter] public EventCallback OnThemeToggle { get; set; }

    [Inject] private DashBoardService DashBoardService { get; set; } = null!;
    [Inject] private DatabaseState DatabaseState { get; set; } = null!;

    private int _totalGames;

    protected override async Task OnInitializedAsync()
    {
        DatabaseState.OnChange += OnDatabaseChanged;

        await LoadTotalGamesAsync();
    }
    
    public void Dispose()
    {
        DatabaseState.OnChange -= OnDatabaseChanged;
    }

    private void OnDatabaseChanged()
    {
        _ = InvokeAsync(async () =>
        {
            await LoadTotalGamesAsync();
            StateHasChanged();
        });
    }

    private async Task LoadTotalGamesAsync()
    {
        _totalGames = await DashBoardService.GetTotalGamesAsync();
    }
}