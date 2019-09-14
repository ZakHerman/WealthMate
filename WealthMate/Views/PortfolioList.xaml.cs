using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioList : ContentPage
    {
        public ObservableCollection<OwnedStock> Stocks { get; } = new ObservableCollection<OwnedStock>();
        public PortfolioList()
        {
            //InitializeComponent();
            GenerateStockExample();
            //OwnedAssetListView.ItemsSource = Stocks;
            //OwnedTDListView.ItemsSource = TermDeposits;
        }

        internal void GenerateStockExample()
        {

            Stock stockTestB = new Stock { Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f };
            Stock stockTestA = new Stock { Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f };
            string name = stockTestA.CompanyName;
            float currentPrice = stockTestA.CurrentPrice;
            float priceClosed = stockTestA.PriceClose;
            Stocks.Add(new OwnedStock(stockTestA, new System.DateTime(2019, 08, 14, 0, 0, 0), 4.5f, 14f));

        }
    }
}