using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using Android.Views;
using WealthMate.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ResolutionGroupName ("WealthMate")]
[assembly:ExportEffect (typeof(DisableShift), "DisableShiftEffect")]
namespace WealthMate.Droid
{
    internal class DisableShift : PlatformEffect
    {
        protected override void OnAttached ()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            // Disable android default shifting
            bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
        }

        protected override void OnDetached ()
        {
        }
    }
}