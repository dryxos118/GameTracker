using Microsoft.AspNetCore.Components.Routing;

namespace GameTracker.Web.App.Models;

public sealed class NavigationItem
{
    public string Href { get; set; } = "/";
    public string Text { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
    public bool Disabled { get; set; }
}