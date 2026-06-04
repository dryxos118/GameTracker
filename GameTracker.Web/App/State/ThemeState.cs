namespace GameTracker.Web.App.State;

public sealed class ThemeState
{
    public bool IsInitialized { get; private set; }

    public bool IsDarkMode { get; private set; } = true;

    public string PrimaryColor { get; private set; } = "indigo";

    public event Action? OnChange;

    public Task SetDarkModeAsync(bool value)
    {
        if (IsDarkMode == value)
            return Task.CompletedTask;

        IsDarkMode = value;
        NotifyStateChanged();

        return Task.CompletedTask;
    }

    public Task SetPrimaryColorAsync(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            value = "indigo";

        if (PrimaryColor == value)
            return Task.CompletedTask;

        PrimaryColor = value;
        NotifyStateChanged();

        return Task.CompletedTask;
    }

    public Task InitializeAsync(bool isDarkMode, string primaryColor)
    {
        IsDarkMode = isDarkMode;
        PrimaryColor = string.IsNullOrWhiteSpace(primaryColor)
            ? "indigo"
            : primaryColor;

        IsInitialized = true;
        NotifyStateChanged();

        return Task.CompletedTask;
    }

    public Task MarkAsInitializedAsync()
    {
        IsInitialized = true;
        NotifyStateChanged();

        return Task.CompletedTask;
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}