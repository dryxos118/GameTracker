using System.Text.Json;
using GameTracker.Web.App.Models;
using Microsoft.JSInterop;

namespace GameTracker.Web.App.Services;

public sealed class ThemePersistenceService(IJSRuntime js)
{
    private readonly IJSRuntime _js = js;

    private const string StorageKey = "game-tracker-theme";

    public async Task<ThemeSettings?> GetThemeSettingsAsync()
    {
        string? json = await _js.InvokeAsync<string?>(
            "localStorage.getItem",
            StorageKey);

        return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<ThemeSettings>(json);
    }

    public async Task SaveThemeSettingsAsync(
        ThemeSettings settings)
    {
        string json = JsonSerializer.Serialize(settings);

        await _js.InvokeVoidAsync(
            "localStorage.setItem",
            StorageKey,
            json);
    }
}