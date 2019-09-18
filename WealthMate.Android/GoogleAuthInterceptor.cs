using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using WealthMate.Auth;
using WealthMate.Auth;
using Xamarin.Auth;

namespace WealthMate.Droid
{
	[Activity(Label = "GoogleAuthInterceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(
		new[] { Intent.ActionView },
		Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
		DataSchemes = new[] { "com.googleusercontent.apps.472528467231-ak6fobbq39tq3uqifr5ekktnf2frh3tf" },
		DataPath = "/oauth2redirect")]
	public class GoogleAuthInterceptor : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
            CustomTabsConfiguration.CustomTabsClosingMessage = null;

			base.OnCreate(savedInstanceState);

			// Convert Android.Net.Url to Uri
			var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
			AuthenticationState.Authenticator.OnPageLoading(uri);

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

			Finish();
		}
	}
}
