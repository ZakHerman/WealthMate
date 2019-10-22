using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;
using Syncfusion.XForms.ComboBox;
using WealthMate.Services;
using System.Collections.ObjectModel;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnedAssetDetailsPage
    {
        public OwnedAsset OwnedAsset { get; }
        public CustomDatePicker CustDate { get; set; }
 
        private SfNumericTextBox editInterestRate;          //Text box for editing details.
        private SfNumericTextBox editLength;
        private SfNumericTextBox editRegPayments;
        private SfNumericTextBox editReturnGoal;
        private SfNumericTextBox editPrincipalValue;
        private int editCompoundRate = -1;

        public OwnedAssetDetailsPage(OwnedAsset ownedAsset)     //Passes selected owned asset to know what details to display
        {
            OwnedAsset = ownedAsset;
            OwnedAsset.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();

            editPrincipalValue = new SfNumericTextBox { Value = 0 };
            editPrincipalValue.ValueChanged += Handle_PrincipalValueChanged;

            editInterestRate = new SfNumericTextBox {Value = 0};
            editInterestRate.ValueChanged += Handle_InterestRateChanged;

            editLength = new SfNumericTextBox {Value = 0};
            editLength.ValueChanged += Handle_LengthChanged;

            editRegPayments = new SfNumericTextBox {Value = 0};
            editRegPayments.ValueChanged += Handle_RegularPaymentsChanged;

            editReturnGoal = new SfNumericTextBox { Value = 0 };
            editReturnGoal.ValueChanged += Handle_ReturnGoalChanged;
        }

        private void DatePicker_Clicked(object sender, EventArgs e)
        {
            Date.IsOpen = !Date.IsOpen;
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

            //var newPurchaseDate;
            var newCompoundRate = editCompoundRate;
            var newPrincipalValue = float.Parse(editPrincipalValue.Value.ToString());
            var newInterestRate = float.Parse(editInterestRate.Value.ToString()) / 100;
            var newLength = int.Parse(editLength.Value.ToString());
            var newReturnGoal = float.Parse(editReturnGoal.Value.ToString());

            UpdatePurchaseDate();
            OwnedAsset.EditAsset(newPrincipalValue, newInterestRate, newCompoundRate, newLength, newReturnGoal);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();
        }
        private void Handle_PrincipalValueChanged(object sender, ValueEventArgs e)
        {
            editPrincipalValue.Value = e.Value.ToString();
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                switch (selectedIndex)
                {
                    case 0:
                        editCompoundRate = 1;
                        break;
                    case 1:
                        editCompoundRate = 2;
                        break;
                    case 2:
                        editCompoundRate = 4;
                        break;
                    case 3:
                        editCompoundRate = 12;
                        break;
                    default:
                        editCompoundRate = 0;
                        break;
                }
            }
        }

        public void UpdatePurchaseDate()
        {
            var selectedItem = Date.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            int monthInt = 0;

            switch (month)
            {
                case "Jan":
                    monthInt = 1;
                    break;
                case "Feb":
                    monthInt = 2;
                    break;
                case "Mar":
                    monthInt = 3;
                    break;
                case "Apr":
                    monthInt = 4;
                    break;
                case "May":
                    monthInt = 5;
                    break;
                case "Jun":
                    monthInt = 6;
                    break;
                case "Jul":
                    monthInt = 7;
                    break;
                case "Aug":
                    monthInt = 8;
                    break;
                case "Sep":
                    monthInt = 9;
                    break;
                case "Oct":
                    monthInt = 10;
                    break;
                case "Nov":
                    monthInt = 11;
                    break;
                case "Dec":
                    monthInt = 12;
                    break;
            }

            string day = selectedItem[1].ToString();
            int dayInt = Int32.Parse(day);

            string year = selectedItem[2].ToString();
            int yearInt = Int32.Parse(year);

            OwnedAsset.PurchaseDate = new DateTime(yearInt, monthInt, dayInt);
        }
    }
}