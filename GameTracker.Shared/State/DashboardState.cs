using GameTracker.Shared.Features.Dashboard;
using GameTracker.Shared.Features.Dashboard.Dtos;

namespace GameTracker.Shared.State;

public sealed class DashboardState(DashBoardService dashBoardService)
{
    private readonly DashBoardService _dashBoardService = dashBoardService;

    public DashboardStatsDto? Stats { get; private set; }
    public List<ContinuePlayingGameDto> ContinuePlayingGames { get; private set; } = [];
    public List<RecentGameDto> RecentGames { get; private set; } = [];
    public DashboardSummaryDto? Summary { get; private set; }

    private bool IsLoaded => Stats is not null;
    public bool IsLoading { get; private set; }

    public event Action? OnChange;

    public async Task LoadAsync(bool forceRefresh = false)
    {
        if (IsLoaded && !forceRefresh)
            return;

        IsLoading = true;
        NotifyStateChanged();

        Stats = await _dashBoardService.GetStatsAsync();
        ContinuePlayingGames = await _dashBoardService.GetContinuePlayingGamesAsync();
        RecentGames = await _dashBoardService.GetRecentGamesAsync();
        Summary = await _dashBoardService.GetSummaryAsync();

        IsLoading = false;
        NotifyStateChanged();
    }

    public async Task RefreshAsync()
    {
        await LoadAsync(forceRefresh: true);
    }

    public void Clear()
    {
        Stats = null;
        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}