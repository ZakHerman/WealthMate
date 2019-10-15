using System;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.ViewModels;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public OwnedStock OwnedStock { get; set; }

        public StockDetailsPage(Stock stock)
        {
            OwnedStock = new OwnedStock{Stock = stock, PurchaseDate = DateTime.Now, AssetName = stock.CompanyName};

            BindingContext = new StockDetailsViewModel(stock);

            InitializeComponent();
        }

        //Asks for details of shares purchased
        private void AddToPortfolioClicked(object sender, EventArgs e)
        {
            StockPortfolioForm.IsOpen = true;
        }

        //Adds purchased shares of stock to the users portfolio
        private void AddInPopupClicked(object sender, EventArgs e)
        {
            if (OwnedStock.PurchasedPrice == 0)
            {
                NullValueErrorPopup.IsOpen = true;
            }
            else
            {
                OwnedStock.UpdateOwnedAsset();
                ((App)Application.Current).User.Portfolio.OwnedAssets.Add(OwnedStock);
                StockPortfolioForm.IsOpen = false;
            }
        }

        private void Handle_NumSharesChanged(object sender, ValueEventArgs e)
        {
            int.TryParse(e.Value.ToString(), out var value);
            OwnedStock.SharesPurchased = value;
        }

        private void Handle_PriceChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedStock.PurchasedPrice = value;
        }

        public void Handle_ReturnGoalChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedStock.ReturnGoal = value;
        }
    }
}