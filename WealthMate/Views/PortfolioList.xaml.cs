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
        public ObservableCollection<OwnedStock> OwnedStock { get; set; }
        public PortfolioList()
        {
            InitializeComponent();
            GenerateStockExample();
            //OwnedAssetListView.ItemsSource = Stocks;
            //OwnedTDListView.ItemsSource = TermDeposits;
        }

        internal void GenerateStockExample()
        {

            OwnedStock = new ObservableCollection<OwnedStock>();

            Stock stockTestB = new Stock { Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f };
            Stock stockTestA = new Stock { Symbol = "SPK", CompanyName = "Spark", CurrentPrice = 2.2f };

            OwnedStock.Add(new OwnedStock(stockTestA, new System.DateTime(2019, 08, 14, 0, 0, 0), 4.5f, 14f));
            OwnedStock.Add(new OwnedStock(stockTestB, new System.DateTime(2019, 09, 14, 0, 0, 0), 6f, 3f));

        }
    }
}