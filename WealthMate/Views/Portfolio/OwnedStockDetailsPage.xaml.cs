using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WealthMate.Views.Markets.Modal;
using WealthMate.Views.Portfolio.Modal;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnedStockDetailsPage
    {
        public OwnedStock OwnedStock { get; }
        public Stock Stock { get; }

        public OwnedStockDetailsPage(OwnedStock ownedStock)
        {
            OwnedStock = ownedStock;
            OwnedStock.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();
        }

        // Event handler for edit stock button, enables popup
        private void EditStockClicked(object sender, EventArgs e)       
        {
            Navigation.PushModalAsync(new EditStockDetailsModalPage(OwnedStock));
        }

        // Remove Stock from portfolio
        private async void RemoveStockClicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Remove Asset", "Are you sure you want to remove this asset from your portfolio?", "Yes", "No");

            if (confirm)
            {
                ((App)Application.Current).User.Portfolio.OwnedAssets.Remove(OwnedStock);
                ((App)Application.Current).User.Portfolio.UpdatePortfolio();

                // Pop owned asset details page off the stack
                await Navigation.PopAsync();
            }
        }
    }
}