using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class OwnedStockDetailsPage : ContentPage
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
        private void EditStockClicked(object sender, System.EventArgs e)       
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button
        protected void OnSaveButtonClicked(object sender, EventArgs args)
        {
            popupLayout.IsOpen = false;

           // float newNumShares = float.Parse(editNumOfShares.Value.ToString());
           // float newPrice = float.Parse(editPurchasePrice.Value.ToString());

           // OwnedStock editedStock = new OwnedStock(Stock, System.DateTime.Now, newNumShares, newPrice);
           // get current stock, replace with editedStock
           //((App)Application.Current).User.Portfolio.AddAsset(newStock);
        }
    }
}