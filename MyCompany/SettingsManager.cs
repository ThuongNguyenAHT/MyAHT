using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace MyCompany
{
    public static class SettingsManager
    {
        public static string AppLanguage
        {
            get => Preferences.Get("AppLanguage", "vi");
            set
            {
                Preferences.Set("AppLanguage", value);
                ApplyLanguage(value);
            }
        }

        public static void ApplyLanguage(string language)
        {
            if (Application.Current.Resources.MergedDictionaries.Count > 0)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
            }

            string resourcePath = $"Resources/Localization/Translations.xaml";
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(resourcePath, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        public static void ApplyTheme(bool isDarkMode)
        {
            App.Current.UserAppTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
            Preferences.Set("AppTheme", isDarkMode ? "Dark" : "Light");
        }
    }
}
