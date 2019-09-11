using System.ComponentModel;
using System.Collections.ObjectModel;
using WealthMate.ViewModels;
using WealthMate.Models;
using Xamarin.Forms;
using Syncfusion;

namespace WealthMate.Views
{
    /**LIST VIEW needs to show:
    * STOCK or TERM DEPOSIT
    * CurrentValue (stocks * current price)
    * Total Return (smaller font)
    * Total ReturnRate (smaller font - this is a percentage)
    */
    [DesignTimeVisible(false)]
    public partial class PortfolioPage
    {
        public float TotalValue { get; set; }
        public ObservableCollection<OwnedStock> Stocks { get; } = new ObservableCollection<OwnedStock>();
        public ObservableCollection<OwnedAsset> TermDeposits { get; } = new ObservableCollection<OwnedAsset>();

        public PortfolioPage()
        {
            TotalValue = 634635.5623f;
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
               
            );

            NavBarTitle.BindingContext = this;
            GenerateStockExample();
            GenerateTermDepositExample();
            //OwnedAssetListView.ItemsSource = Stocks;
            OwnedTDListView.ItemsSource = TermDeposits;



            BindingContext = new PieChart();
        }

        public void GenerateTermDepositExample()
        {
            TermDeposits.Add(new OwnedAsset("Westpac",new System.DateTime(2019,09,09,0,0,0),"Term Deposit",12f,1.2f,12,1,3));
            TermDeposits.Add(new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3));
        }

        public void GenerateStockExample()
        {

            Stock stockTestB = new Stock { Symbol = "BFG", CompanyName = "Burger Fuel", CurrentPrice = 42.2f };
            Stock stockTestA = new Stock { Symbol = "SPK", CompanyName = stockTestB.CompanyName, CurrentPrice = 2.2f };
            string name = stockTestA.CompanyName;
            float currentPrice = stockTestA.CurrentPrice;
            float priceClosed = stockTestA.PriceClose;
            Stocks.Add(new OwnedStock(ref name, new System.DateTime(2019, 08, 14, 0, 0, 0), 4.5f, 14f, ref currentPrice, ref priceClosed));

        }
    }
}