using GameTracker.Web.App.Configurations;
using GameTracker.Web.App.Models;
using GameTracker.Web.App.Services;
using GameTracker.Web.App.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Web.App.Providers;

public partial class AppProviders : ComponentBase, IDisposable
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [Inject] private ThemeState ThemeState { get; set; } = null!;
    [Inject] private ThemePersistenceService ThemePersistence { get; set; } = null!;


    private MudTheme _theme = new();
    private string _currentPrimaryColor = string.Empty;

    protected override void OnInitialized()
    {
        BuildTheme();

        ThemeState.OnChange += OnThemeChange;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        DateTime start = DateTime.UtcNow;

        ThemeSettings? settings =
            await ThemePersistence.GetThemeSettingsAsync();

        TimeSpan elapsed = DateTime.UtcNow - start;

        TimeSpan minimumLoading = TimeSpan.FromSeconds(1);

        if (elapsed < minimumLoading)
        {
            await Task.Delay(minimumLoading - elapsed);
        }

        if (settings is not null)
        {
            await ThemeState.InitializeAsync(
                settings.IsDarkMode,
                settings.PrimaryColor);
        }
        else
        {
            await ThemeState.MarkAsInitializedAsync();
        }
    }

    private void OnThemeChange()
    {
        if (_currentPrimaryColor != ThemeState.PrimaryColor)
        {
            BuildTheme();
        }

        _ = InvokeAsync(async () =>
        {
            if (ThemeState.IsInitialized)
            {
                await ThemePersistence.SaveThemeSettingsAsync(
                    new ThemeSettings
                    {
                        IsDarkMode = ThemeState.IsDarkMode,
                        PrimaryColor = ThemeState.PrimaryColor
                    });
            }

            StateHasChanged();
        });
    }

    private void BuildTheme()
    {
        _currentPrimaryColor = ThemeState.PrimaryColor;
        _theme = ThemeConfiguration.Create(_currentPrimaryColor);
    }

    public void Dispose()
    {
        ThemeState.OnChange -= OnThemeChange;
    }
}