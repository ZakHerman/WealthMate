using System;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.Services;
using System.Collections.ObjectModel;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnedStockDetailsPage
    {
        public OwnedStock OwnedStock { get; }
        public Stock Stock { get; }
        public CustomDatePicker CustDate { get; set; }

        private SfNumericTextBox editNumOfShares;               //Textboxes for editing ownedstock details
        private SfNumericTextBox editPurchasePrice;
        private SfNumericTextBox editReturnGoal;

        public OwnedStockDetailsPage(OwnedStock ownedStock)
        {
            OwnedStock = ownedStock;
            OwnedStock.UpdateOwnedAsset();
            BindingContext = this;
            InitializeComponent();

            editNumOfShares = new SfNumericTextBox {Value = 0};
            editNumOfShares.ValueChanged += Handle_NumSharesChanged;

            editPurchasePrice = new SfNumericTextBox {Value = 0};
            editPurchasePrice.ValueChanged += Handle_PriceChanged;

            editReturnGoal = new SfNumericTextBox { Value = 0 };
            editReturnGoal.ValueChanged += Handle_ReturnGoalChanged;
        }

        private void DatePicker_Clicked(object sender, EventArgs e)
        {
            Date.IsOpen = !Date.IsOpen;
        }

        // Event handler for edit stock button, enables popup
        private void EditStockClicked(object sender, EventArgs e)       
        {
            popupLayout.IsOpen = true;
        }

        // Event handler for save editing button, will update current stock
        protected void SaveInPopupClicked(object sender, EventArgs e)
        {
            popupLayout.IsOpen = false;

            var newNumShares = float.Parse(editNumOfShares.Value.ToString());
            var newPrice = float.Parse(editPurchasePrice.Value.ToString());
            var newReturnGoal = float.Parse(editReturnGoal.Value.ToString());

            UpdatePurchaseDate();
            OwnedStock.EditStock(newNumShares, newPrice, newReturnGoal);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();
        }

        private void Handle_NumSharesChanged(object sender, ValueEventArgs e)
        {
            editNumOfShares.Value = e.Value.ToString();
        }

        private void Handle_PriceChanged(object sender, ValueEventArgs e)
        {
            editPurchasePrice.Value = e.Value.ToString();
        }

        private void Handle_ReturnGoalChanged(object sender, ValueEventArgs e)
        {
            editReturnGoal.Value = e.Value.ToString();
        }

        //Remove Stock from portfolio
        private void RemoveStockClicked(object sender, EventArgs e)
        {
            RemoveStockConfirmationBox.IsOpen = true;
        }

        private async void PopupAcceptRemoveClicked(object sender, EventArgs e)
        {
            ((App)Application.Current).User.Portfolio.OwnedAssets.Remove(OwnedStock);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();
            RemoveStockConfirmationBox.IsOpen = false;

            // Pop owned stock details page off the stack
            await Navigation.PopAsync();
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

            OwnedStock.PurchaseDate = new DateTime(yearInt, monthInt, dayInt);
        }
    }
}