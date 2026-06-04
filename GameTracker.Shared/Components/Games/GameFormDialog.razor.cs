using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Common;
using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Features.Games;
using GameTracker.Shared.Features.Games.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Shared.Components.Games;

public partial class GameFormDialog : AppComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    
    [Parameter,EditorRequired] public string Title { get; set; }
    [Parameter] public GameLibraryStatus InitialLibraryStatus { get; set; } = GameLibraryStatus.Library;
    [Parameter] public int? GameId { get; set; }

    [Inject] private GameService GameService { get; set; } = null!;
    [Inject] private LookupService LookupService { get; set; } = null!;

    private GameFormDto Model { get; set; } = new();
    private List<LookupItemDto> Platforms { get; set; } = [];
    private List<LookupItemDto> Launchers { get; set; } = [];
    private List<LookupItemDto> Genres { get; set; } = [];
    private List<LookupItemDto> Tags { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        if (GameId.HasValue)
        {
            Model = await GameService.GetGameFormAsync(GameId.Value);
        }
        else
        {
            Model.LibraryStatus = InitialLibraryStatus;
        }

        Platforms = await LookupService.GetPlatformsAsync();
        if (Model.PlatformId is null)
        {
            Model.PlatformId = Platforms
                .FirstOrDefault(x => x.Name == "PC")
                ?.Id;
        }

        Launchers = await LookupService.GetLaunchersAsync();
        Genres = await LookupService.GetGenresAsync();
        Tags = await LookupService.GetTagsAsync();
    }

    private void Cancel() => MudDialog.Cancel();

    private void Submit()
    {
        if (!string.IsNullOrEmpty(Model.Title))
        {
            MudDialog.Close(DialogResult.Ok(Model));
        }
    }
}