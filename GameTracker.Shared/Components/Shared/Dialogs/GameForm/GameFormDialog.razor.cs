using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.Features.Platforms;
using GameTracker.Shared.Features.Taxonomy;
using GameTracker.Shared.UI.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Shared.Components.Shared.Dialogs.GameForm;

public partial class GameFormDialog : AppComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] [EditorRequired] public string Title { get; set; } = string.Empty;
    [Parameter] public GameLibraryStatus InitialLibraryStatus { get; set; } = GameLibraryStatus.Library;
    [Parameter] public int? GameId { get; set; }
    [Parameter] public DialogMode Mode { get; set; } = DialogMode.Add;

    [Inject] private GameService GameService { get; set; } = null!;
    [Inject] private PlatformService PlatformService { get; set; } = null!;
    [Inject] private TaxonomyService TaxonomyService { get; set; } = null!;

    private GameFormDto Model { get; set; } = new();
    private List<SelectItemDto<int>> Platforms { get; set; } = [];
    private List<SelectItemDto<int>> Launchers { get; set; } = [];
    private List<SelectItemDto<int>> Genres { get; set; } = [];
    private List<SelectItemDto<int>> Tags { get; set; } = [];

    private bool ReadOnly => Mode == DialogMode.ReadOnly;

    protected override async Task OnInitializedAsync()
    {
        if (GameId.HasValue)
        {
            Model = await GameService.GetGameFormAsync(GameId.Value);
        }
        else
        {
            Model.LibraryStatus = InitialLibraryStatus;
            if (InitialLibraryStatus == GameLibraryStatus.Wishlist)
                Model.PlayStatus = GamePlayStatus.Backlog;
        }

        Platforms = await PlatformService.GetPlatformSelectItemsAsync();
        if (Model.PlatformId is null)
            Model.PlatformId = Platforms
                .FirstOrDefault(x => x.Text == "PC")
                ?.Value;

        Launchers = await PlatformService.GetLauncherSelectItemsAsync();
        Genres = await TaxonomyService.GetGenreSelectItemsAsync();
        Tags = await TaxonomyService.GetTagSelectItemsAsync();
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Submit()
    {
        if (!string.IsNullOrEmpty(Model.Title)) MudDialog.Close(DialogResult.Ok(Model));
    }
}