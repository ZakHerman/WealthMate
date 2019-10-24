using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WealthMate.Views.Portfolio.Modal;

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
        private async void EditAssetClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditAssetModalPage(OwnedAsset));
        }

        // Remove Asset from portfolio
        private async void RemoveAssetClicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Remove Asset", "Are you sure you want to remove this asset from your portfolio?", "Yes", "No");

            if (confirm)
            {
                ((App)Application.Current).User.Portfolio.OwnedAssets.Remove(OwnedAsset);
                ((App)Application.Current).User.Portfolio.UpdatePortfolio();

                // Pop owned asset details page off the stack
                await Navigation.PopAsync();
            }
        }
    }
}