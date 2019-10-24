using Syncfusion.Licensing;
using WealthMate.Views;
using WealthMate.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using WealthMate.Helpers;
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

            SyncfusionLicenseProvider.RegisterLicense("MTUzNzM5QDMxMzcyZTMzMmUzMEhGM29ILzZFaUc3MGppQUdzMUlEZDJIamhjNStBUGJldmhBUlNYODRySEE9");
            InitializeComponent();

            ThemeChanger = themeChanger;
            MainPage = new MainPage();
        }

        public App()
        {
        }

        protected override async void OnStart()
        {
            Settings.Load();

            var watchedStocks = await Database.GetWatchListAsync();

            foreach (var watchedStock in watchedStocks)
            {
                WatchList.Add(await Database.GetStockAsync(watchedStock.Symbol));
            }

            var ownedAssets = await Database.GetOwnedAssetsAsync();
            var ownedStocks = await Database.GetOwnedStocksAsync();

            foreach (var ownedAsset in ownedAssets)
            {
                User.Portfolio.OwnedAssets.Add(ownedAsset);
            }

            foreach (var ownedStock in ownedStocks)
            {
                User.Portfolio.OwnedAssets.Add(ownedStock);
            }

            User.Portfolio.UpdatePortfolio();
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
