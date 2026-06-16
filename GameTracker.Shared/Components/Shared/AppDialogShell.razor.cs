using GameTracker.Shared.Components.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Shared.Components.Shared;

public partial class AppDialogShell : AppComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] [EditorRequired] public string Title { get; set; } = string.Empty;
    [Parameter] public string Icon { get; set; } = Icons.Material.Filled.SportsEsports;
    [Parameter] public Color Color { get; set; } = Color.Primary;

    [Parameter] public string ConfirmText { get; set; } = "Confirmer";
    [Parameter] public string CancelText { get; set; } = "Annuler";
    [Parameter] public string? Style { get; set; }

    [Parameter] [EditorRequired] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public EventCallback OnConfirm { get; set; }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task Confirm()
    {
        if (OnConfirm.HasDelegate)
        {
            await OnConfirm.InvokeAsync();
            return;
        }

        MudDialog.Close(DialogResult.Ok(true));
    }
}