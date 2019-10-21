using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using WealthMate.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace WealthMate.Droid
{
    [Activity(Label = "WealthMate", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IThemeChanger
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App(this));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void ApplyTheme(Theme theme)
        {
            Xamarin.Forms.Application.Current.Resources.TryGetValue("AndroidMenuColor", out var backgroundResource);
            var backgroundColor = (Color?) backgroundResource ?? Color.FromHex("#CC212121");

            SetStatusBarColor(backgroundColor);

            SetTheme(theme == WealthMate.Theme.Dark
                ? Resource.Style.Base_Theme_AppCompat
                : Resource.Style.Base_Theme_AppCompat_Light);
        }

        private void SetStatusBarColor(Color color)
        {
            Window.SetStatusBarColor(color.ToAndroid());
            Window.SetNavigationBarColor(color.ToAndroid());
        }
    }
}