using GameTracker.Web.App.Models;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

namespace GameTracker.Web.App.Configurations;

public static class RoutesConfiguration
{
    public const string Dashboard = "/";
    public const string Games = "/games";
    public const string Wishlist = "/wishlist";
    public const string Platforms = "/platforms";
    public const string Taxonomy = "/taxonomy";
    public const string Settings = "/settings";

    public static readonly IReadOnlyList<NavigationItem> NavItems =
    [
        new() { Href = Dashboard, Text = "Dashboard", Icon = Icons.Material.Filled.Dashboard, Match = NavLinkMatch.All },
        new() { Href = Games, Text = "Bibliothèque", Icon = Icons.Material.Filled.VideogameAsset },
        new() { Href = Wishlist, Text = "Wishlist", Icon = Icons.Material.Filled.FavoriteBorder },
        new() { Href = Platforms, Text = "Plateformes", Icon = Icons.Material.Filled.Devices },
        new() { Href = Taxonomy, Text = "Genres & Tags", Icon = Icons.Material.Filled.Category },
        new() { Href = Settings, Text = "Paramètres", Icon = Icons.Material.Filled.Settings },
    ];
}