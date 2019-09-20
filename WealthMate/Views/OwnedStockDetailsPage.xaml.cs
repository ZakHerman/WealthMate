using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class OwnedStockDetailsPage : ContentPage
    {
        public OwnedStock OwnedStock { get; }
        public Stock Stock { get; }

        private SfNumericTextBox editNumOfShares;
        private SfNumericTextBox editPurchasePrice;
        public OwnedStockDetailsPage(OwnedStock ownedStock)
        {
            OwnedStock = ownedStock;
            OwnedStock.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();

            editNumOfShares = new SfNumericTextBox();
            editNumOfShares.Value = 0;
            editNumOfShares.ValueChanged += Handle_NumSharesChanged;

            editPurchasePrice = new SfNumericTextBox();
            editPurchasePrice.Value = 0;
            editPurchasePrice.ValueChanged += Handle_PriceChanged;
        }

        // Event handler for edit stock button, enables popup
        private void EditStockClicked(object sender, System.EventArgs e)       
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button, will update current stock
        protected void SaveInPopupClicked(object sender, System.EventArgs e)
        {
            popupLayout.IsOpen = false;

            long newNumShares = long.Parse(editNumOfShares.Value.ToString());
            float newPrice = float.Parse(editPurchasePrice.Value.ToString());

            OwnedStock.EditStock(newNumShares, newPrice, OwnedStock);
            OwnedStock.UpdateOwnedAsset();
        }

        private void CancelInPopupClicked(object sender, EventArgs args)
        {
            popupLayout.IsOpen = false;
        }

        private void Handle_NumSharesChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editNumOfShares.Value = e.Value.ToString();
        }

        private void Handle_PriceChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editNumOfShares.Value = e.Value.ToString();
        }
    }
}