using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Components.Shared.Dialogs.GameForm;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.State;
using GameTracker.Shared.UI.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Web.Components.Pages;

public partial class Wishlist : AppComponentBase, IDisposable
{
    [Inject] private WishlistState WishlistState { get; set; } = null!;
    [Inject] private GameService GameService { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        WishlistState.OnChange += StateHasChanged;

        if (!WishlistState.IsLoaded)
            await WishlistState.LoadAsync();
    }

    private async Task OpenCreateDialog()
    {
        DialogParameters parameters = new()
        {
            [nameof(GameFormDialog.Title)] = "Ajouter un jeu à la liste de souhaits",
            [nameof(GameFormDialog.InitialLibraryStatus)] = GameLibraryStatus.Wishlist,
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
        await WishlistState.LoadAsync();

        Snackbar.Add(
            $"'{dto.Title}' ajouté à la liste de souhaits.",
            Severity.Success);
    }

    public void Dispose()
    {
        WishlistState.OnChange -= StateHasChanged;
    }
}