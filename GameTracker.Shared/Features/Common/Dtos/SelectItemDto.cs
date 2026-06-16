namespace GameTracker.Shared.Features.Common.Dtos;

public sealed class SelectItemDto<TValue>
{
    public SelectItemDto()
    {
    }

    public SelectItemDto(string text, TValue value)
    {
        Text = text;
        Value = value;
    }

    public string Text { get; set; } = string.Empty;

    public TValue Value { get; set; } = default!;
}