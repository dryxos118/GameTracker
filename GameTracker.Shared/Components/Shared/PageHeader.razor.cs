using GameTracker.Shared.Components.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameTracker.Shared.Components.Shared;

public partial class PageHeader : AppComponentBase
{
    [Parameter, EditorRequired] public string Title { get; set; } = string.Empty;

    [Parameter] public string? SubTitle { get; set; } = string.Empty;

    [Parameter] public string? Icon { get; set; }

    [Parameter] public Color Color { get; set; } = Color.Primary;

    [Parameter] public bool IsLarge { get; set; } = false;

    [Parameter] public RenderFragment? HeaderContent { get; set; }
}