using Syncfusion.Licensing;
using WealthMate.Views;
using WealthMate.Models;
using System;

namespace WealthMate
{
    public partial class App
    {
        public User User { get; }

        public App()
        {
            User = new User();

            SyncfusionLicenseProvider.RegisterLicense("MTM5ODg3QDMxMzcyZTMyMmUzME9aQWh6eUtsMTNNdUlRd3VZVDFyNHovZ0hyY0RVQ29IY0FUQkE1Y0hZbjg9");
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
