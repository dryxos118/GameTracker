using MudBlazor;

namespace GameTracker.Shared.UI.Models;

public sealed class AppMessageDialogModel
{
    public string Title { get; set; } = string.Empty;

    public string? SubTitle { get; set; }

    public string Message { get; set; } = string.Empty;

    public string ConfirmText { get; set; } = "Confirmer";

    public string CancelText { get; set; } = "Annuler";

    public string Icon { get; set; } = Icons.Material.Filled.Info;

    public Color Color { get; set; } = Color.Primary;
}