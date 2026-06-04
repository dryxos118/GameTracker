using GameTracker.Web.App.State;
using Microsoft.AspNetCore.Components;

namespace GameTracker.Web.Components.Layout;

public partial class AppLayout : LayoutComponentBase
{
    [Inject] private ThemeState ThemeState { get; set; } = null!;
    
    private bool _drawerOpen;

    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    private void SetDrawerOpen(bool value)
    {
        _drawerOpen = value;
    }
}