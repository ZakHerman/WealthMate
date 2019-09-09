using System.Collections.ObjectModel;
using System.ComponentModel;
using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class FinancialPage
    {
        public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();
        public ObservableCollection<TermDeposit> TermDeposits { get; } = new ObservableCollection<TermDeposit>();

        public FinancialPage()
        {
            InitializeComponent();

            // Event handler to show details page when asset is tapped
            WatchlistView.ItemTapped += WatchlistView_ItemTapped;

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
            );

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSmoothScrollEnabled(false); //Disable default scrolling animation for button press

            NavBarTitle.BindingContext = new PortfolioPage();
            GenerateStockExample();
            GenerateTermDepositExample();
            WatchlistView.ItemsSource = Stocks;
            TermDepositlistView.ItemsSource = TermDeposits;
        }

        private async void WatchlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        // Dummy data to use before using database
        private void GenerateStockExample()
        {
            Stocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f});
            Stocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f});
            Stocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 2});
            Stocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 1});
            Stocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 0.01f});
            Stocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 0f});
            Stocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f});
            Stocks.Add(new Stock {Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f});
            Stocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f});
            Stocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 3});
            Stocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 15f});
            Stocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 4});
            Stocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f});
            Stocks.Add(new Stock {Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f});
        }

        public void GenerateTermDepositExample()
        {
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 12.5f});
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 3});
        }
    }
}