using System.Collections.ObjectModel;
using System.ComponentModel;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class FinancialPage
    {
        public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();

        public FinancialPage()
        {
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
            );

            On<Android>().SetIsSmoothScrollEnabled(false); //Disable default scrolling animation for button press

            WatchlistView.ItemsSource = Stocks;
            Stocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f});
            Stocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f});
            Stocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 52521.23f});
            Stocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 63791.23f});
            Stocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 22522.52f});
            Stocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f});
            Stocks.Add(new Stock {Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "WBC", CompanyName = "Westpac", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f});
            Stocks.Add(new Stock {Symbol = "AIR", CompanyName = "Air New Zealand", CurrentPrice = 4f});
            Stocks.Add(new Stock {Symbol = "ANZ", CompanyName = "ANZ", CurrentPrice = 52521.23f});
            Stocks.Add(new Stock {Symbol = "AIA", CompanyName = "Auckland International Airport", CurrentPrice = 6352525252791.23f});
            Stocks.Add(new Stock {Symbol = "MCY", CompanyName = "Mercury", CurrentPrice = 22522.52f});
            Stocks.Add(new Stock {Symbol = "TLS", CompanyName = "Telstra", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "SKT", CompanyName = "Sky Network Television", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "GNE", CompanyName = "Genesis Energy", CurrentPrice = 1.23f});
            Stocks.Add(new Stock {Symbol = "AMP", CompanyName = "AMP", CurrentPrice = 2.52f});
            Stocks.Add(new Stock {Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f});
            Stocks.Add(new Stock {Symbol = "CNU", CompanyName = "Chorus", CurrentPrice = 1.23f});
        }
    }
}