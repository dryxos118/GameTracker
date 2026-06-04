namespace GameTracker.Shared.Domain.Enums;

[Flags]
public enum GameMode
{
    None = 0,
    Solo = 1,
    Multiplayer = 2,
    Coop = 4,
    Online = 8,
    LocalMultiplayer = 16
}