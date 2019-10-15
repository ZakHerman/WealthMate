using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnedStockDetailsPage
    {
        public OwnedStock OwnedStock { get; }
        public Stock Stock { get; }

        private SfNumericTextBox editNumOfShares;               //Textboxes for editing ownedstock details
        private SfNumericTextBox editPurchasePrice;
        private SfNumericTextBox editReturnGoal;

        public OwnedStockDetailsPage(OwnedStock ownedStock)
        {
            OwnedStock = ownedStock;
            OwnedStock.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();

            editNumOfShares = new SfNumericTextBox {Value = 0};
            editNumOfShares.ValueChanged += Handle_NumSharesChanged;

            editPurchasePrice = new SfNumericTextBox {Value = 0};
            editPurchasePrice.ValueChanged += Handle_PriceChanged;

            editReturnGoal = new SfNumericTextBox { Value = 0 };
            editReturnGoal.ValueChanged += Handle_ReturnGoalChanged;
        }

    
        // Event handler for edit stock button, enables popup
        private void EditStockClicked(object sender, EventArgs e)       
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button, will update current stock
        protected void SaveInPopupClicked(object sender, EventArgs e)
        {
            popupLayout.IsOpen = false;

            var newNumShares = int.Parse(editNumOfShares.Value.ToString());
            var newPrice = float.Parse(editPurchasePrice.Value.ToString());

            OwnedStock.EditStock(newNumShares, newPrice, OwnedStock);
            OwnedStock.UpdateOwnedAsset();
        }

        private void Handle_NumSharesChanged(object sender, ValueEventArgs e)
        {
            editNumOfShares.Value = e.Value.ToString();
        }

        private void Handle_PriceChanged(object sender, ValueEventArgs e)
        {
            editPurchasePrice.Value = e.Value.ToString();
        }

        private void Handle_ReturnGoalChanged(object sender, ValueEventArgs e)
        {
            editReturnGoal.Value = e.Value.ToString();
        }

        //Remove Stock from portfolio
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