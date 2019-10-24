using System;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using WealthMate.ViewModels;
using WealthMate.Views.Markets.Modal;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public Stock Stock { get; set; }
        public StockDetailsPage(Stock stock)
        {
            Stock = stock;
           
            BindingContext = new StockDetailsViewModel(stock);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = false;
        }

        // Asks for details of shares purchased
        private async void AddToPortfolioClicked(object sender, EventArgs e)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await Navigation.PushModalAsync(new StockDetailsModalPage(Stock));
        }
    }
}