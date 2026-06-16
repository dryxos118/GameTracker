using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Components.Games;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.State;
using GameTracker.Shared.UI.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Web.Components.Pages;

public partial class Games : AppComponentBase, IDisposable
{
    [Inject] private GameService GameService { get; set; } = null!;
    [Inject] private GameState GameState { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        GameState.OnChange += StateHasChanged;

        if (!GameState.IsLoaded)
            await GameState.LoadAsync();
    }

    public void Dispose()
    {
        GameState.OnChange -= StateHasChanged;
    }

    private async Task OpenCreateDialog()
    {
        DialogParameters parameters = new()
        {
            [nameof(GameFormDialog.Title)] = "Ajouter un jeu",
            [nameof(GameFormDialog.InitialLibraryStatus)] = GameLibraryStatus.Library,
            [nameof(GameFormDialog.Mode)] = DialogMode.Add
        };

        IDialogReference dialog =
            await DialogService.ShowAsync<GameFormDialog>(null, parameters, new DialogOptions());

        DialogResult? result = await dialog.Result;

        if (result is null || result.Canceled)
            return;


        if (result.Data is not GameFormDto dto)
        {
            Snackbar.Add(
                "Impossible de récupérer les données du jeu.",
                Severity.Error);
            return;
        }

        await GameService.AddAsync(dto);
        await GameState.RefreshAsync();

        Snackbar.Add(
            $"Le jeu '{dto.Title}' a été ajouté.",
            Severity.Success);
    }
}