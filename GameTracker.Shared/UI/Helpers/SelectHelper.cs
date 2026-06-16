using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.UI.Enums;

namespace GameTracker.Shared.UI.Helpers;

public static class SelectHelper
{
    public static readonly List<SelectItemDto<GameLibraryStatus>> GameLibraryStatuses =
    [
        new("Bibliothèque", GameLibraryStatus.Library),
        new("Wishlist", GameLibraryStatus.Wishlist)
    ];

    public static readonly List<SelectItemDto<GamePlayStatus?>> GamePlayStatuses =
    [
        new("Backlog", GamePlayStatus.Backlog),
        new("En cours", GamePlayStatus.Playing),
        new("En pause", GamePlayStatus.Paused),
        new("Terminé", GamePlayStatus.Completed),
        new("Abandonné", GamePlayStatus.Dropped)
    ];

    public static readonly List<SelectItemDto<GameMode>> GameModes =
    [
        new("Aucun", GameMode.None),
        new("Solo", GameMode.Solo),
        new("Multijoueur", GameMode.Multiplayer),
        new("Coop", GameMode.Coop),
        new("En ligne", GameMode.Online),
        new("Local", GameMode.LocalMultiplayer)
    ];

    public static readonly List<SelectItemDto<GameSortBy>> GameSortBy =
    [
        new("Récents", Enums.GameSortBy.Recent),
        new("Titre", Enums.GameSortBy.Title),
        new("Note", Enums.GameSortBy.Rating),
        new("Heures jouées", Enums.GameSortBy.Hours)
    ];
}