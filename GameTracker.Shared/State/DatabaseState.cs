namespace GameTracker.Shared.State;

public sealed class DatabaseState
{
    public event Action? OnChange;

    public void NotifyDatabaseChanged()
    {
        OnChange?.Invoke();
    }
}