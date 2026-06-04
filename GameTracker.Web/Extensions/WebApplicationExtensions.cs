namespace GameTracker.Web.Extensions;

public static class WebApplicationExtensions
{
    public static void AddAppLocalization(this WebApplication app)
    {
        Dictionary<string, string> cultures = new()
        {
            { "en-US", "English" },
            { "fr-FR", "French" },
        };

        string[] supportedCultures = cultures.Keys.ToArray();

        RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[1]).AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
        
        app.UseRequestLocalization(localizationOptions);
    }
}