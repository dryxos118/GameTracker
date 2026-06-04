using GameTracker.Shared.Domain.Entities;
using GameTracker.Shared.Domain.Enums;
using GameTracker.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Shared.Infrastructure.Seed;

public static class DatabaseSeeder
{
    public static async Task<SeedResult> SeedAsync(AppDbContext context)
    {
        int platforms = 0;
        int launchers = 0;
        int genres = 0;
        int tags = 0;
        int games = 0;

        if (!await context.Platforms.AnyAsync())
        {
            context.Platforms.AddRange(
                new Platform { Name = "PC" },
                new Platform { Name = "PlayStation" },
                new Platform { Name = "Xbox" },
                new Platform { Name = "Nintendo Switch" },
                new Platform { Name = "Steam Deck" }
            );

            platforms = 5;
        }

        if (!await context.Launchers.AnyAsync())
        {
            context.Launchers.AddRange(
                new Launcher { Name = "Steam" },
                new Launcher { Name = "Epic Games" },
                new Launcher { Name = "GOG" },
                new Launcher { Name = "Heroic" },
                new Launcher { Name = "Lutris" },
                new Launcher { Name = "Bottles" },
                new Launcher { Name = "Itch.io" }
            );

            launchers = 7;
        }

        if (!await context.Genres.AnyAsync())
        {
            context.Genres.AddRange(
                new Genre { Name = "RPG" },
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "FPS" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Simulation" },
                new Genre { Name = "Sandbox" }
            );

            genres = 7;
        }

        if (!await context.Tags.AnyAsync())
        {
            context.Tags.AddRange(
                new Tag { Name = "Open World" },
                new Tag { Name = "Story Rich" },
                new Tag { Name = "Souls Like" },
                new Tag { Name = "Survival" },
                new Tag { Name = "Sci-Fi" },
                new Tag { Name = "Fantasy" }
            );

            tags = 6;
        }

        await context.SaveChangesAsync();

        if (!await context.Games.AnyAsync())
        {
            Platform pcPlatform = context.Platforms.First(x => x.Name == "PC");

            Launcher steamLauncher = context.Launchers.First(x => x.Name == "Steam");
            Launcher heroicLauncher = context.Launchers.First(x => x.Name == "Heroic");

            Genre rpgGenre = context.Genres.First(x => x.Name == "RPG");
            Genre actionGenre = context.Genres.First(x => x.Name == "Action");
            Genre sandboxGenre = context.Genres.First(x => x.Name == "Sandbox");

            Tag fantasyTag = context.Tags.First(x => x.Name == "Fantasy");
            Tag openWorldTag = context.Tags.First(x => x.Name == "Open World");
            Tag sciFiTag = context.Tags.First(x => x.Name == "Sci-Fi");

            Game skyrim = new()
            {
                Title = "Skyrim Special Edition",
                Description = "Open world fantasy RPG.",
                PlatformId = pcPlatform.Id,
                LauncherId = steamLauncher.Id,
                LibraryStatus = GameLibraryStatus.Library,
                PlayStatus = GamePlayStatus.Playing,
                Modes = GameMode.Solo,
                PlayedHours = 120,
                Rating = 9,
                IsFavorite = true,
                CoverUrl =
                    "https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/489830/header.jpg?t=1753715778",

                GameGenres =
                [
                    new GameGenre { GenreId = rpgGenre.Id },
                    new GameGenre { GenreId = actionGenre.Id }
                ],

                GameTags =
                [
                    new GameTag { TagId = fantasyTag.Id },
                    new GameTag { TagId = openWorldTag.Id }
                ]
            };

            Game eliteDangerous = new()
            {
                Title = "Elite Dangerous",
                Description = "Space simulation sandbox.",
                PlatformId = pcPlatform.Id,
                LauncherId = steamLauncher.Id,
                LibraryStatus = GameLibraryStatus.Library,
                PlayStatus = GamePlayStatus.Completed,
                Modes = GameMode.Solo | GameMode.Online,
                PlayedHours = 340,
                Rating = 8,
                IsFavorite = true,
                CoverUrl =
                    "https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/359320/header.jpg?t=1759842966",

                GameGenres =
                [
                    new GameGenre { GenreId = sandboxGenre.Id }
                ],

                GameTags =
                [
                    new GameTag { TagId = sciFiTag.Id }
                ]
            };

            Game cyberpunk = new()
            {
                Title = "Cyberpunk 2077",
                Description = "Futuristic open world RPG.",
                PlatformId = pcPlatform.Id,
                LauncherId = heroicLauncher.Id,
                LibraryStatus = GameLibraryStatus.Wishlist,
                PlayStatus = GamePlayStatus.Backlog,
                Modes = GameMode.Solo,
                PlayedHours = 0,
                Rating = null,
                IsFavorite = false,
                CoverUrl =
                    "https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1091500/e9047d8ec47ae3d94bb8b464fb0fc9e9972b4ac7/header.jpg?t=1769690377",

                GameGenres =
                [
                    new GameGenre { GenreId = rpgGenre.Id },
                    new GameGenre { GenreId = actionGenre.Id }
                ],

                GameTags =
                [
                    new GameTag { TagId = sciFiTag.Id },
                    new GameTag { TagId = openWorldTag.Id }
                ]
            };

            context.Games.AddRange(
                skyrim,
                eliteDangerous,
                cyberpunk
            );

            await context.SaveChangesAsync();
        }

        return new SeedResult(
            HasSeeded: platforms + launchers + genres + tags + games > 0,
            Platforms: platforms,
            Launchers: launchers,
            Genres: genres,
            Tags: tags,
            Games: games);
    }
}