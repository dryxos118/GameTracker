using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Components.Shared.Dialogs.GameForm;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.State;
using GameTracker.Shared.UI.Enums;
using GameTracker.Shared.UI.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Web.Components.Pages;

public partial class Wishlist : AppComponentBase, IDisposable
{
    [Inject] private WishlistState WishlistState { get; set; } = null!;
    [Inject] private GameState GameState { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        WishlistState.OnChange += StateHasChanged;

        if (!WishlistState.IsLoaded)
            await WishlistState.LoadAsync();
    }

    public void Dispose()
    {
        WishlistState.OnChange -= StateHasChanged;
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

        if (result is { Canceled: false, Data: GameFormDto dto })
        {
            OperationResult success = await GameState.AddAsync(dto);

            if (success == OperationResult.Success)
                Snackbar.Add($"{dto.Title} à été ajouté.", Severity.Success);
            else
                Snackbar.Add(MessageHelper.GenericError, Severity.Error);
        }
    }
}