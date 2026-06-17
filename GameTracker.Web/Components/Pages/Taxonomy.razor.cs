using GameTracker.Shared.Components.Bases;
using GameTracker.Shared.State;
using Microsoft.AspNetCore.Components;

namespace GameTracker.Web.Components.Pages;

public partial class Taxonomy : AppComponentBase, IDisposable
{
    [Inject] private TaxonomyState TaxonomyState { get; set; } = null!;

    public void Dispose()
    {
        TaxonomyState.OnChange -= StateHasChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        TaxonomyState.OnChange += StateHasChanged;

        if (!TaxonomyState.IsLoaded)
            await TaxonomyState.LoadAsync();
    }
}