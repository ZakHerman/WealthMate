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
    public partial class OwnedAssetDetailsPage
    {
        public OwnedAsset OwnedAsset { get; }

        // Passes selected owned asset to know what details to display
        public OwnedAssetDetailsPage(OwnedAsset ownedAsset)
        {
            OwnedAsset = ownedAsset;
            OwnedAsset.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();
        }

        // Event handler for edit stock button, enables popup
        private void EditAssetClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EditAssetDetailsModalPage(OwnedAsset));
        }


        // Remove Asset from portfolio
        private void RemoveAssetClicked(object sender, EventArgs e)
        {
            RemoveAssetConfirmationBox.IsOpen = true;
        }

        private async void PopupAcceptRemoveClicked(object sender, EventArgs e)
        {
            ((App)Application.Current).User.Portfolio.OwnedAssets.Remove(OwnedAsset);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();
            RemoveAssetConfirmationBox.IsOpen = false;

            // Pop owned asset details page off the stack
            await Navigation.PopAsync();
        }
    }
}