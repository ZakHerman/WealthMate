using System;
using Plugin.Toast;
using WealthMate.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WealthMate.Helpers
{
    public class Helper
    {
        public static void DisplayToastNotification(string message)
        {
            Application.Current.Resources.TryGetValue("ToastNotificationBackgroundColor", out var backgroundResource);
            Application.Current.Resources.TryGetValue("ToastNotificationTextColor", out var textResource);

            var backgroundColor = backgroundResource != null ? ((Color)backgroundResource).ToHex() : "#CC212121";
            var textColor = textResource != null ? ((Color)textResource).ToHex() : "#FFFFFF";

            CrossToastPopUp.Current.ShowCustomToast(message, backgroundColor, textColor);
        }

        public static void SetTheme(string val)
        {
            Enum.TryParse(val, out Theme theme);
            Preferences.Set("Theme", val); // Save theme setting

            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries == null)
                return;

            mergedDictionaries.Clear();

            // Set android material theme and add custom resources
            switch (theme)
            {
                case Theme.Dark:
                    mergedDictionaries.Add(new DarkTheme());
                    ((App)Application.Current).ThemeChanger.ApplyTheme(Theme.Dark);
                    break;
                default:
                    mergedDictionaries.Add(new LightTheme());
                    ((App)Application.Current).ThemeChanger.ApplyTheme(Theme.Light);
                    break;
            }
        }
    }
}
