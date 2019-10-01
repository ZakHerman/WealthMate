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
            InitializeDummyUserPortfolio();                 //Temporary while we can't add Assets yet

            SyncfusionLicenseProvider.RegisterLicense("MTM5ODg3QDMxMzcyZTMyMmUzME9aQWh6eUtsMTNNdUlRd3VZVDFyNHovZ0hyY0RVQ29IY0FUQkE1Y0hZbjg9");
            InitializeComponent();

            MainPage = new MainPage();
        }

        public void InitializeDummyUserPortfolio()
        {
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0));
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Westpac", new System.DateTime(2019, 09, 09, 0, 0, 0), "Term Deposit", 12f, 1.2f, 12, 1, 3));
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3));
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock{CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4}, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100));
            User.Portfolio.UpdatePortfolio();
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
