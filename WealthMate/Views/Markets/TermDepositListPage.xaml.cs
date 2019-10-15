using Syncfusion.SfNumericTextBox.XForms;
using System.Collections.Generic;
using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.ComboBox;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositListPage
    {
        private SearchBar _searchBar;

        private List<string> compoundRates { get; set; }
        private OwnedAsset OwnedAsset { get; set; }
        private TermDeposit TermDeposit { get; set; }

        SfComboBox comboBox;

        public TermDepositListPage()
        {
            LoadTermDeposits();
            InitializeComponent();
            compoundRates = new List<string>();
            compoundRates.Add("Annually");
            compoundRates.Add("Semi-Annually");
            compoundRates.Add("Quarterly");
            compoundRates.Add("Monthly");
            compoundRates.Add("Never");
        }

        private async void LoadTermDeposits()
        {
            await DataService.FetchTermDepositsAsync();
            TermDepositList.ItemsSource = DataService.TermDeposits;
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);//set sender to SearchBar

            if (TermDepositList.DataSource != null)
            {
                TermDepositList.DataSource.Filter = FilterTDeposits;//filters the data source
                TermDepositList.DataSource.RefreshFilter(); // refreshes the view
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> object representing a search return
        /// <returns></returns> boolean value for checking for text in the serach bar
        private bool FilterTDeposits(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }

            return obj is TermDeposit termD && (termD.Provider.ToLower().Contains(_searchBar.Text.ToLower()));
        }

        //Handles a term deposit being clicked from the term deposit page
        private void TermDepositClicked(object sender, ItemTappedEventArgs e)
        {
            TermDeposit = (TermDeposit)e.ItemData;
            OwnedAsset = new OwnedAsset(TermDeposit.Provider, System.DateTime.Now, "Term Deposit", 0f, TermDeposit.InterestRate, TermDeposit.LengthInMonths, 0, 0f, 0f);
            var picker = new Picker { Title = "Compound RateA: ", TitleColor = Color.Red };
            //picker.ItemsSource = compoundRates;
            
            comboBox = new SfComboBox();
            comboBox.ComboBoxSource = compoundRates;

            AddTDForm.IsOpen = true;

        }

        //Handles when the "Add" button inside the pop-up is clicked to initiate adding the term deposit to the users profile
        private void AddInPopupClicked(object sender, System.EventArgs e)
        {
            if (OwnedAsset.PrincipalValue == 0)
            {
                NullValueErrorPopup.IsOpen = true;
            }
            else
            {
                OwnedAsset.UpdateOwnedAsset();
                ((App)Application.Current).User.Portfolio.OwnedAssets.Add(OwnedAsset);
                AddTDForm.IsOpen = false;
            }
        }

        //Handles the "Amount Purchased" field in the term deposit popup being changed by the user
        private void Handle_InvestAmountChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedAsset.PrincipalValue = value;
        }

        private void Handle_GoalAmountChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedAsset.ReturnGoal = value;
        }

        private void Handle_dropdownSelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            //DisplayAlert("Selection Changed", "SelectedIndex: " + comboBox.SelectedIndex, "OK");

            switch(comboBox.SelectedIndex)
            {
                case 1:
                    OwnedAsset.CompoundRate = 1;
                    break;
                case 2:
                    OwnedAsset.CompoundRate = 2;
                    break;
                case 3:
                    OwnedAsset.CompoundRate = 4;
                    break;
                case 4:
                    OwnedAsset.CompoundRate = 12;
                    break;
                default:
                    OwnedAsset.CompoundRate = 0;
                    break;

            }
        }



    }
}