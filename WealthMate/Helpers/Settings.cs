using Xamarin.Essentials;

namespace WealthMate.Helpers
{
    class Settings
    {
        public static void Load()
        {
            Helper.SetTheme(Preferences.Get("Theme", "Light"));
        }
    }
}
