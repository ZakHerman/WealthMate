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
        private SfNumericTextBox editRePeriod;
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

            editRePeriod = new SfNumericTextBox();
            editRePeriod.Value = 0;
            editRePeriod.ValueChanged += Handle_ReinvestmentPeriodChanged;

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

        // Event handler for save editing button
        protected void SaveInPopupClicked(object sender, EventArgs args)
        {
            popupLayout.IsOpen = false;

            float newInterestRate = float.Parse(editInterestRate.Value.ToString());
            float newRePeriod = float.Parse(editRePeriod.Value.ToString());
            float newLength = float.Parse(editLength.Value.ToString());
            float newRegPayments = float.Parse(editRegPayments.Value.ToString());

            // 1. Check which asset 
            // 2. If term deposit, new term deposit + replace
            // 3. get current owned asset, replace with new owned asset
            // 4. ((App)Application.Current).User.Portfolio.AddAsset(newAsset);
        }

        private void CancelInPopupClicked(object sender, EventArgs args)
        {
            popupLayout.IsOpen = false;
        }

        private void Handle_InterestRateChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editInterestRate.Value = e.Value.ToString();
        }

        private void Handle_ReinvestmentPeriodChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editRePeriod.Value = e.Value.ToString();
        }

        private void Handle_LengthChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editLength.Value = e.Value.ToString();
        }

        private void Handle_RegularPaymentsChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value.ToString());
            editRegPayments.Value = e.Value.ToString();
        }
    }
}