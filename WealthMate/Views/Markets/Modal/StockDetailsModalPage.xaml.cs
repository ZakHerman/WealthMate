using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Markets.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsModalPage
    {
        public Stock Stock { get; set; }

        public StockDetailsModalPage(Stock stock)
        {
            Stock = stock;

            InitializeComponent();
        }

        private async void CancelButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void AddButton_OnClicked(object sender, EventArgs e)
        {
            int.TryParse(SharesEntry.Text, out var shares);
            float.TryParse(PriceEntry.Text, out var price);
            float.TryParse(GoalEntry.Text, out var goal);
            var date = PurchaseDate.Date;

            if (shares <= 0)
                await DisplayAlert(null, "Please enter number of shares purchased!", "OK");
            else if (price <= 0)
                await DisplayAlert (null, "Please enter purchase price!", "OK");
            else
            {
                var os = new OwnedStock(Stock, date, price, shares, goal);
                ((App)Application.Current).User.Portfolio.AddAsset(os);
                await Navigation.PopModalAsync();
            }
        }
    }
}