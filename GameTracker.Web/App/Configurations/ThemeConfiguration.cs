using MudBlazor;

namespace GameTracker.Web.App.Configurations;

public static class ThemeConfiguration
{
    public static MudTheme Create(string primaryColor)
    {
        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = GetLightPrimary(primaryColor),
                Secondary = "#8B5CF6",
                Tertiary = "#06B6D4",

                Background = "#F8FAFC",
                Surface = "#FFFFFF",

                AppbarBackground = "#FFFFFF",
                DrawerBackground = "#FFFFFF",

                TextPrimary = "#0F172A",
                TextSecondary = "#64748B",

                Divider = "#E2E8F0",
                LinesDefault = "#E2E8F0",

                Success = "#10B981",
                Warning = "#F59E0B",
                Error = "#EF4444",
                Info = "#3B82F6"
            },

            PaletteDark = new PaletteDark
            {
                Primary = GetDarkPrimary(primaryColor),
                Secondary = "#A78BFA",
                Tertiary = "#22D3EE",
                
                PrimaryContrastText = "#0F172A",
                SecondaryContrastText = "#0F172A",
                TertiaryContrastText = "#0F172A",

                Background = "#0B1120",
                Surface = "#111827",

                AppbarBackground = "#0F172A",
                DrawerBackground = "#0F172A",

                TextPrimary = "#F8FAFC",
                TextSecondary = "#CBD5E1",

                Divider = "#1E293B",
                LinesDefault = "#1E293B",

                Success = "#34D399",
                Warning = "#FBBF24",
                Error = "#F87171",
                Info = "#60A5FA",
                
                SuccessContrastText = "#0F172A",
                WarningContrastText = "#0F172A",
                InfoContrastText = "#0F172A",
            },

            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "8px"
            }
        };
    }

    private static string GetLightPrimary(string color)
    {
        return color switch
        {
            "violet" => "#8B5CF6",
            "cyan" => "#06B6D4",
            "emerald" => "#10B981",
            "teal" => "#14B8A6",
            _ => "#6366F1"
        };
    }

    private static string GetDarkPrimary(string color)
    {
        return color switch
        {
            "violet" => "#A78BFA",
            "cyan" => "#22D3EE",
            "emerald" => "#34D399",
            "teal" => "#2DD4BF",
            _ => "#818CF8"
        };
    }
}