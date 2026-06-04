using GameTracker.Shared.Domain.Entities;
using GameTracker.Shared.Features.Games.Dtos;
using GameTracker.Shared.UI;
using GameTracker.Shared.UI.Enums;

namespace GameTracker.Shared.Features.Games;

public static class GameMapper
{
    public static GameFormDto ToDto(this Game game)
    {
        return new GameFormDto
        {
            Id = game.Id,

            // INFO
            Title = game.Title,
            Description = game.Description,
            CoverUrl = game.CoverUrl,
            ReleaseDate = game.ReleaseDate,

            // LIBRARY
            LibraryStatus = game.LibraryStatus,
            PlayStatus = game.PlayStatus,

            IsFavorite = game.IsFavorite,
            Rating = game.Rating ?? 0,
            PlayedHours = game.PlayedHours,

            // CLASSIFICATION
            PlatformId = game.PlatformId,
            LauncherId = game.LauncherId,
            Modes = game.Modes,

            GenreIds = game.GameGenres
                .Select(x => x.GenreId)
                .ToList(),

            TagIds = game.GameTags
                .Select(x => x.TagId)
                .ToList()
        };
    }

    public static Game ToEntity(this GameFormDto dto)
    {
        Game game = new()
        {
            Id = dto.Id ?? 0,

            // INFO
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description)
                ? null
                : dto.Description.Trim(),

            CoverUrl = BuildCoverUrl(dto),
            ReleaseDate = dto.ReleaseDate,

            // LIBRARY
            LibraryStatus = dto.LibraryStatus,
            PlayStatus = dto.PlayStatus,

            IsFavorite = dto.IsFavorite,
            Rating = dto.Rating,
            PlayedHours = dto.PlayedHours,

            // CLASSIFICATION
            PlatformId = dto.PlatformId!.Value,
            LauncherId = dto.LauncherId,
            Modes = dto.Modes,
        };

        game.GameGenres = dto.GenreIds
            .Distinct()
            .Select(x => new GameGenre
            {
                GenreId = x,
                Game = game
            })
            .ToList();

        game.GameTags = dto.TagIds
            .Distinct()
            .Select(x => new GameTag
            {
                TagId = x,
                Game = game
            })
            .ToList();

        return game;
    }

    private static string? BuildCoverUrl(GameFormDto dto)
    {
        return dto.CoverSource switch
        {
            GameCoverSource.Url => string.IsNullOrWhiteSpace(dto.CoverUrl)
                ? null
                : dto.CoverUrl.Trim(),

            GameCoverSource.SteamAppId when dto.SteamAppId is not null =>
                $"https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/{dto.SteamAppId}/header.jpg",

            _ => null
        };
    }
}