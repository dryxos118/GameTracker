using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Features.Settings;
using GameTracker.Shared.Features.Settings.Dtos;
using GameTracker.Shared.Infrastructure.Seed;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Shared.Components.Settings;

public partial class DatabaseSettingsSection : AppComponentBase
{
    [Inject] private SettingsService SettingsService { get; set; } = null!;

    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    
    private async Task SeedDemoData()
    {
        SeedResult result = await SettingsService.SeedDemoDataAsync();

        if (!result.HasSeeded)
        {
            Snackbar.Add("Des données existent déjà.", Severity.Info);
            return;
        }

        Snackbar.Add(
            $"Données de démo ajoutées : {result.Games} jeux, {result.Platforms} plateformes, {result.Launchers} launchers.",
            Severity.Success);
    }

    private async Task ResetDatabase(bool seedDemoData)
    {
        ResetDatabaseResult result =
            await SettingsService.ResetDatabaseAsync(seedDemoData);

        Snackbar.Add(
            seedDemoData
                ? L["Settings.Database.ResetWithDemoSuccess"]
                : L["Settings.Database.ResetSuccess"],
            seedDemoData
                ? Severity.Success
                : Severity.Warning);
    }
}