using Syncfusion.Licensing;
using WealthMate.Views;
using WealthMate.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using WealthMate.Services;

namespace WealthMate
{
    public partial class App
    {
        public User User { get; }
        public static ObservableCollection<Stock> WatchList { get; set; } = new ObservableCollection<Stock>();

        private static LocalDatabase _database;

        public static LocalDatabase Database =>
            _database ?? (_database = new LocalDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WealthMate.db3")));

        public readonly IThemeChanger ThemeChanger;

        public App(IThemeChanger themeChanger)
        {
            User = new User();
            InitializeDummyUserPortfolio();

            SyncfusionLicenseProvider.RegisterLicense("MTUzNzM5QDMxMzcyZTMzMmUzMEhGM29ILzZFaUc3MGppQUdzMUlEZDJIamhjNStBUGJldmhBUlNYODRySEE9");
            InitializeComponent();

            ThemeChanger = themeChanger;
            MainPage = new MainPage();
        }

        public void InitializeDummyUserPortfolio()
        {
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0, 100f));
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Westpac", new System.DateTime(2019, 09, 09, 0, 0, 0), "Term Deposit", 12f, 1.2f, 12, 1, 3, 2f));
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3, 2f));
            User.Portfolio.OwnedAssets.Add(new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40, 200));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock{CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 42.4f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100, 1000f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f));
            User.Portfolio.OwnedAssets.Add(new OwnedStock(new Stock{CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 42.4f}, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100, 1000f));
            User.Portfolio.UpdatePortfolio();
        }

        protected override async void OnStart()
        {
            var watchedStocks = await Database.GetWatchListAsync();

            foreach (var watchedStock in watchedStocks)
            {
                WatchList.Add(await Database.GetStockAsync(watchedStock.Symbol));
            }
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
