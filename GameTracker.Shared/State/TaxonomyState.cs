using GameTracker.Shared.Features.Common.Dtos;
using GameTracker.Shared.Features.Common.Enums;
using GameTracker.Shared.Features.Taxonomy;
using GameTracker.Shared.Features.Taxonomy.Dtos;

namespace GameTracker.Shared.State;

public sealed class TaxonomyState(TaxonomyService taxonomyService)
{
    private readonly TaxonomyService _taxonomyService = taxonomyService;

    public bool IsLoading { get; private set; }

    public bool IsLoaded { get; private set; }

    public List<GenreListDto> Genres { get; private set; } = [];

    public List<TagListDto> Tags { get; private set; } = [];

    public event Action? OnChange;

    public async Task LoadAsync()
    {
        IsLoading = true;
        IsLoaded = false;
        NotifyStateChanged();

        Genres = await _taxonomyService.GetGenresAsync();
        Tags = await _taxonomyService.GetTagsAsync();

        IsLoading = false;
        IsLoaded = true;
        NotifyStateChanged();
    }

    public async Task<OperationResult> AddAsync(TaxonomyReferenceType type, NameFormDto dto)
    {
        OperationResult result = await _taxonomyService.AddAsync(type, dto);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    public async Task<OperationResult> UpdateAsync(TaxonomyReferenceType type, NameFormDto dto)
    {
        OperationResult result = await _taxonomyService.UpdateAsync(type, dto);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    public async Task<OperationResult> DeleteAsync(TaxonomyReferenceType type, int id)
    {
        OperationResult result = await _taxonomyService.DeleteAsync(type, id);

        if (result == OperationResult.Success)
            await LoadAsync();

        return result;
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}