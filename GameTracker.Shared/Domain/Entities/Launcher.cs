namespace GameTracker.Shared.Domain.Entities;

public class Launcher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Icon { get; set; }

    public List<Game> Games { get; set; } = [];
}