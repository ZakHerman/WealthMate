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

        // Event handler for edit stock button, enables popupview
        private void EditStockClicked(object sender, System.EventArgs e)        
        {
            overlay.IsVisible = true;
            activityIndicator.IsRunning = true;
        }

        // Event handler for cancel editing button
        protected void OnCancelButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
        }

        // Event handler for save editing button
        protected void OnSaveButtonClicked(object sender, EventArgs args)
        {
            int firstVal = Convert.ToInt32(editNumOfShares.Text);
            double secondVal = Convert.ToDouble(editPurchasePrice.Text);

            float newN = (float)firstVal;
            float newP = (float)secondVal;

            OwnedStock editedStock = new OwnedStock(Stock, System.DateTime.Now, newN, newP);
            // get current stock, replace with editedStock
            //((App)Application.Current).User.Portfolio.AddAsset(newStock);
        }
    }
}