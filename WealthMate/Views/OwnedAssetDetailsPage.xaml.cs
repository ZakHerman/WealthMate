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
    public partial class OwnedAssetDetailsPage : ContentPage
    {
        public OwnedAsset OwnedAsset { get; }
        public OwnedAssetDetailsPage(OwnedAsset ownedAsset)
        {
            OwnedAsset = ownedAsset;
            OwnedAsset.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();
        }

        // Event handler for edit stock button, enables popup
        private void EditAssetClicked(object sender, System.EventArgs e)
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button
        protected void OnSaveButtonClicked(object sender, EventArgs args)
        {
            popupLayout.IsOpen = false;

            // float newInterestRate = float.Parse(editInterestRate.Value.ToString());
            // float newRePeriod = float.Parse(editRePeriod.Value.ToString());
            // float newLength = float.Parse(editLength.Value.ToString());
            // float newRegPayments = float.Parse(editRegPayments.Value.ToString());

            // 1. Check which asset 
            // 2. If term deposit, new term deposit + replace
            // 3. get current owned asset, replace with new owned asset
            // 4. ((App)Application.Current).User.Portfolio.AddAsset(newAsset);
        }
    }
}