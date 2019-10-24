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
        private async void RemoveAssetClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Remove Asset", "Are you sure you want to remove this asset from your portfolio?", "Yes", "No");

            if (answer)
            {
                ((App)Application.Current).User.Portfolio.OwnedAssets.Remove(OwnedAsset);
                ((App)Application.Current).User.Portfolio.UpdatePortfolio();
                await Navigation.PopAsync();
            }
        }
    }
}