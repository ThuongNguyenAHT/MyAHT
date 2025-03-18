using System.Globalization;
using Microsoft.Maui.Controls;
using MyCompany.Resources.Localization;

namespace MyCompany.Helpers
{
    public class LocalizationResourceManager : BindableObject
    {
        private static readonly LocalizationResourceManager _instance = new LocalizationResourceManager();
        public static LocalizationResourceManager Instance => _instance;

        private LocalizationResourceManager() { }

        public string this[string text] => AppResources.ResourceManager.GetString(text, CultureInfo.CurrentUICulture) ?? text;

        public void SetCulture(string languageCode)
        {
            CultureInfo culture = new CultureInfo(languageCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Preferences.Set("AppLanguage", languageCode);

            // Thông báo cập nhật UI khi đổi ngôn ngữ
            OnPropertyChanged(null);
        }
    }
}
