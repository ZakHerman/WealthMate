using Xamarin.Essentials;
using Xamarin.Forms;

namespace WealthMate.Helpers
{
    public class Settings
    {
        // Loads all settings in the settings page with saved value else default value
        public static void Load()
        {
            Helper.SetTheme(Preferences.Get("Theme", "Light"));
            ((App)Application.Current).User.Portfolio.EditPortfolioGoal(Preferences.Get("PortfolioGoal", 0f));
        }
    }
}
