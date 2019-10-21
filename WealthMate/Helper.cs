using Plugin.Toast;
using Xamarin.Forms;

namespace WealthMate
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
    }
}
