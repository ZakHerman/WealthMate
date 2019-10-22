using System;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.ViewModels;
using System.Collections.ObjectModel;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        private float _sharesPurchased;
        private float _purchasedPrice;
        private DateTime _purchasedDate;
        private float _returnGoal;
        public Stock PublicStock { get; set; }
        public OwnedStock OwnedStock { get; set; }
        public StockDetailsPage(Stock stock)
        {
            PublicStock = stock;
           
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

            var month = selectedItem[0].ToString();
            var monthInt = 0;

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

            var day = selectedItem[1].ToString();
            var dayInt = int.Parse(day);

            var year = selectedItem[2].ToString();
            var yearInt = int.Parse(year);

            _purchasedDate = new DateTime(yearInt, monthInt, dayInt);
        }

        //Asks for details of shares purchased
        private void AddToPortfolioClicked(object sender, EventArgs e)
        {
            StockPortfolioForm.IsOpen = true;
        }

        //Adds purchased shares of stock to the users portfolio
        private void AddInPopupClicked(object sender, EventArgs e)
        {
            if (_purchasedPrice <= 0)
                DisplayAlert (null, "Please enter purchase price!", "OK");
            else if(_sharesPurchased <= 0)
                DisplayAlert(null, "Please enter number of shares purchased!", "OK");
            else
            {
                UpdatePurchaseDate();
                var os = new OwnedStock(PublicStock, _purchasedDate, _purchasedPrice, _sharesPurchased, _returnGoal);
                ((App)Application.Current).User.Portfolio.AddAsset(os);
                StockPortfolioForm.IsOpen = false;
            }
        }

        private void Handle_NumSharesChanged(object sender, ValueEventArgs e)
        {
            int.TryParse(e.Value.ToString(), out var value);
            _sharesPurchased = value;
        }

        private void Handle_PriceChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            _purchasedPrice = value;
        }

        public void Handle_ReturnGoalChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            _returnGoal = value;
        }
    }
}