using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnedAssetDetailsPage
    {
        public OwnedAsset OwnedAsset { get; }

        private SfNumericTextBox editInterestRate;          //Text box for editing details.
        private SfNumericTextBox editLength;
        private SfNumericTextBox editRegPayments;
        private SfNumericTextBox editReturnGoal;

        public OwnedAssetDetailsPage(OwnedAsset ownedAsset)     //Passes selected owned asset to know what details to display
        {
            OwnedAsset = ownedAsset;
            OwnedAsset.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();

            editInterestRate = new SfNumericTextBox {Value = 0};
            editInterestRate.ValueChanged += Handle_InterestRateChanged;

            editLength = new SfNumericTextBox {Value = 0};
            editLength.ValueChanged += Handle_LengthChanged;

            editRegPayments = new SfNumericTextBox {Value = 0};
            editRegPayments.ValueChanged += Handle_RegularPaymentsChanged;

            editReturnGoal = new SfNumericTextBox { Value = 0 };
            editReturnGoal.ValueChanged += Handle_ReturnGoalChanged;
        }

        // Event handler for edit stock button, enables popup
        private void EditAssetClicked(object sender, EventArgs e)
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button, will update current asset
        protected void SaveInPopupClicked(object sender, EventArgs args)
        {
            popupLayout.IsOpen = false;

            var newInterestRate = float.Parse(editInterestRate.Value.ToString());
            var newLength = int.Parse(editLength.Value.ToString());
            var newRegPayments = float.Parse(editRegPayments.Value.ToString());

            OwnedAsset.EditAsset(newInterestRate, newLength, newRegPayments, OwnedAsset);

            //int index = ((App)Application.Current).User.Portfolio.OwnedAssets.IndexOf(OwnedAsset);
            //OwnedAsset oldOA = ((App)Application.Current).User.Portfolio.OwnedAssets.ElementAt(index);
            //((App)Application.Current).User.Portfolio.OwnedAssets.Remove(oldOA);

            //OwnedAsset newEditedOA = new OwnedAsset(OwnedAsset.AssetName, OwnedAsset.PurchaseDate, OwnedAsset.Type, OwnedAsset.PrincipalValue, newInterestRate, newLength, OwnedAsset.CompoundRate, newRegPayments);
            //((App)Application.Current).User.Portfolio.OwnedAssets.Add(newEditedOA);

            OwnedAsset.UpdateOwnedAsset();
        }

        // Collects new interest rate value from text box entered by user
        private void Handle_InterestRateChanged(object sender, ValueEventArgs e)
        {
            editInterestRate.Value = e.Value.ToString();
        }

        // Collects new length value from text box entered by user
        private void Handle_LengthChanged(object sender, ValueEventArgs e)
        {
            editLength.Value = e.Value.ToString();
        }

        // Collects new regular payments value from text box entered by user
        private void Handle_RegularPaymentsChanged(object sender, ValueEventArgs e)
        {
            editRegPayments.Value = e.Value.ToString();
        }

        private void Handle_ReturnGoalChanged(object sender, ValueEventArgs e)
        {
            editReturnGoal.Value = e.Value.ToString();
        }

        //Remove Asset from portfolio
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