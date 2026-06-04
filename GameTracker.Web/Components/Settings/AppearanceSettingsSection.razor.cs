using System.Globalization;
using GameTracker.Shared.Components.Bases;
using GameTracker.Web.App.Models;
using GameTracker.Web.App.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Web.Components.Settings;

public partial class AppearanceSettingsSection : AppComponentBase, IDisposable
{
    [Inject] private ThemeState ThemeState { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    protected override void OnInitialized()
    {
        ThemeState.OnChange += OnThemeChanged;
    }

    private void OnThemeChanged()
    {
        _ = InvokeAsync(StateHasChanged);
    }

    private CultureInfo CurrentCulture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (Equals(CultureInfo.CurrentCulture, value))
                return;

            string uri = new Uri(Navigation.Uri)
                .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);

            string cultureEscaped = Uri.EscapeDataString(value.Name);
            string uriEscaped = Uri.EscapeDataString(uri);

            Navigation.NavigateTo(
                $"culture/set-culture?culture={cultureEscaped}&redirectUri={uriEscaped}",
                forceLoad: true);
        }
    }

    private readonly Dictionary<string, CultureInfo> SupportedCultures = new()
    {
        { "fr-FR", new CultureInfo("fr-FR") },
        { "en-US", new CultureInfo("en-US") },
    };

    private static string GetCultureIcon(CultureInfo culture)
    {
        return culture.Name switch
        {
            "fr-FR" => "app-icon-fr-FR",
            "en-US" => "app-icon-en-US",
            _ => Icons.Material.Filled.Language
        };
    }

    private void SetCulture(CultureInfo? culture)
    {
        if (culture is null)
            return;

        CurrentCulture = culture;
    }

    private string SelectedTheme => ThemeState.IsDarkMode ? "dark" : "light";

    private readonly List<ThemeOption> _themeModes =
    [
        new()
        {
            Value = "dark",
            Label = "Settings.Appearance.Dark",
            Icon = Icons.Material.Filled.DarkMode
        },

        new()
        {
            Value = "light",
            Label = "Settings.Appearance.Light",
            Icon = Icons.Material.Filled.LightMode
        }
    ];

    private async Task ChangeThemeMode(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        await ThemeState.SetDarkModeAsync(value == "dark");
    }

    private readonly Dictionary<string, string> _themeColors = new()
    {
        ["indigo"] = "#6366F1",
        ["violet"] = "#8B5CF6",
        ["cyan"] = "#06B6D4",
        ["emerald"] = "#10B981",
        ["teal"] = "#14B8A6",
    };

    private async Task ChangePrimaryColor(string? value)
    {
        if (string.IsNullOrEmpty(value))
            value = "indigo";

        await ThemeState.SetPrimaryColorAsync(value);
    }

    public void Dispose()
    {
        ThemeState.OnChange -= OnThemeChanged;
    }
}