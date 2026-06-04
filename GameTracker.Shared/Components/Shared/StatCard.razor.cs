using GameTracker.Shared.Components.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Shared.Components.Shared;

public partial class StatCard : AppComponentBase
{
    [Parameter, EditorRequired] public string Title { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string Value { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string Icon { get; set; } = string.Empty;

    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public Color Color { get; set; } = Color.Primary;
    [Parameter] public bool IsLarge { get; set; }
}