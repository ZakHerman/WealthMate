using System;
using System.Diagnostics;
using Newtonsoft.Json;
using WealthMate.Auth;
using WealthMate.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string clientId = null;
			string redirectUri = null;

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					clientId = Config.iOSClientId;
					redirectUri = Config.iOSRedirectUrl;
					break;

				case Device.Android:
					clientId = Config.AndroidClientId;
					redirectUri = Config.AndroidRedirectUrl;
					break;
			}

            //var accounts = await SecureStorageAccountStore.FindAccountsForServiceAsync(Constants.AppName);
            //_account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				Config.Scope,
				new Uri(Config.AuthorizeUrl),
				new Uri(redirectUri ?? throw new InvalidOperationException()),
				new Uri(Config.AccessTokenUrl),
				null,
				true);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			AuthenticationState.Authenticator = authenticator;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
        }

        private async void OnThemeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ThemeSelectionPage());
        }

        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;

			if (e.IsAuthenticated)
			{
				// If the user is authenticated, request their basic user data from Google
				// UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
				var request = new OAuth2Request("GET", new Uri(Config.UserInfoUrl), null, e.Account);
				var response = await request.GetResponseAsync();

				if (response != null)
				{
					// Deserialize the data and store it in the account store
					// The users email address will be used to identify data in SimpleDB
					var userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<User>(userJson);
				}

                await SecureStorageAccountStore.SaveAsync(e.Account, Config.AppName);

                Logout.IsVisible = true;
                Logout.Text = $"Logout, {user?.Email}";
                GoogleLogin.IsVisible = false;
            }
		}

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
		{
            if (sender is OAuth2Authenticator authenticator)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			Debug.WriteLine("Authentication error: " + e.Message);
		}
    }
}