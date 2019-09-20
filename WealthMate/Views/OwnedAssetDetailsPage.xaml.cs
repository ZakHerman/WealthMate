using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;
using System.Collections.ObjectModel;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnedAssetDetailsPage : ContentPage
    {
        public OwnedAsset OwnedAsset { get; }

        private SfNumericTextBox editInterestRate;
        private SfNumericTextBox editLength;
        private SfNumericTextBox editRegPayments;
        public OwnedAssetDetailsPage(OwnedAsset ownedAsset)
        {
            OwnedAsset = ownedAsset;
            OwnedAsset.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();

            editInterestRate = new SfNumericTextBox();
            editInterestRate.Value = 0;
            editInterestRate.ValueChanged += Handle_InterestRateChanged;

            editLength = new SfNumericTextBox();
            editLength.Value = 0;
            editLength.ValueChanged += Handle_LengthChanged;

            editRegPayments = new SfNumericTextBox();
            editRegPayments.Value = 0;
            editRegPayments.ValueChanged += Handle_RegularPaymentsChanged;
        }

        // Event handler for edit stock button, enables popup
        private void EditAssetClicked(object sender, System.EventArgs e)
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button, will update current asset
        protected void SaveInPopupClicked(object sender, System.EventArgs args)
        {
            popupLayout.IsOpen = false;

            float newInterestRate = float.Parse(editInterestRate.Value.ToString());
            int newLength = int.Parse(editLength.Value.ToString());
            float newRegPayments = float.Parse(editRegPayments.Value.ToString());

            OwnedAsset.EditAsset(newInterestRate, newLength, newRegPayments, OwnedAsset);

            //int index = ((App)Application.Current).User.Portfolio.OwnedAssets.IndexOf(OwnedAsset);
            //OwnedAsset oldOA = ((App)Application.Current).User.Portfolio.OwnedAssets.ElementAt(index);
            //((App)Application.Current).User.Portfolio.OwnedAssets.Remove(oldOA);

            //OwnedAsset newEditedOA = new OwnedAsset(OwnedAsset.AssetName, OwnedAsset.PurchaseDate, OwnedAsset.Type, OwnedAsset.PrincipalValue, newInterestRate, newLength, OwnedAsset.CompoundRate, newRegPayments);
            //((App)Application.Current).User.Portfolio.OwnedAssets.Add(newEditedOA);

            OwnedAsset.UpdateOwnedAsset();
        }

        // Collects new interest rate value from text box entered by user
        private void Handle_InterestRateChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editInterestRate.Value = e.Value.ToString();
        }

        // Collects new length value from text box entered by user
        private void Handle_LengthChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editLength.Value = e.Value.ToString();
        }

        // Collects new regular payments value from text box entered by user
        private void Handle_RegularPaymentsChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editRegPayments.Value = e.Value.ToString();
        }
    }
}