using System;
using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.ViewModels;
using System.Collections.ObjectModel;
using System.Collections;
using System.Globalization;

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

        private void DatePicker_Clicked(object sender, EventArgs e)
        {
            Date.IsOpen = !Date.IsOpen;
        }

        public void UpdatePurchaseDate()
        {
            var selectedItem = Date.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            int monthInt = 0;

            switch (month)
            {
                case "Jan":
                    monthInt = 1;
                    break;
                case "Feb":
                    monthInt = 2;
                    break;
                case "Mar":
                    monthInt = 3;
                    break;
                case "Apr":
                    monthInt = 4;
                    break;
                case "May":
                    monthInt = 5;
                    break;
                case "Jun":
                    monthInt = 6;
                    break;
                case "Jul":
                    monthInt = 7;
                    break;
                case "Aug":
                    monthInt = 8;
                    break;
                case "Sep":
                    monthInt = 9;
                    break;
                case "Oct":
                    monthInt = 10;
                    break;
                case "Nov":
                    monthInt = 11;
                    break;
                case "Dec":
                    monthInt = 12;
                    break;
            }

            string day = selectedItem[1].ToString();
            int dayInt = Int32.Parse(day);

            string year = selectedItem[2].ToString();
            int yearInt = Int32.Parse(year);

            OwnedStock.PurchaseDate = new DateTime(yearInt, monthInt, dayInt);
        }

        //Asks for details of shares purchased
        private void AddToPortfolioClicked(object sender, EventArgs e)
        {
            StockPortfolioForm.IsOpen = true;
        }

        //Adds purchased shares of stock to the users portfolio
        private void AddInPopupClicked(object sender, EventArgs e)
        {
            if (OwnedStock.PurchasedPrice <= 0)
            {
                DisplayAlert (null, "Please enter purchase price!", "OK");
            }
            else
            {
                UpdatePurchaseDate();
                OwnedStock.UpdateOwnedAsset();
                ((App)Application.Current).User.Portfolio.AddAsset(OwnedStock);
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