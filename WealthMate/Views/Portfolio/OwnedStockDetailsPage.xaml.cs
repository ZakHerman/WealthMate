using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.Services;
using System.Collections.ObjectModel;
using WealthMate.Views.Markets.Modal;

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
        private void RemoveStockClicked(object sender, EventArgs e)
        {
            RemoveStockConfirmationBox.IsOpen = true;
        }

        private async void PopupAcceptRemoveClicked(object sender, EventArgs e)
        {
            ((App)Application.Current).User.Portfolio.OwnedAssets.Remove(OwnedStock);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();
            RemoveStockConfirmationBox.IsOpen = false;

            // Pop owned stock details page off the stack
            await Navigation.PopAsync();
        }
    }
}