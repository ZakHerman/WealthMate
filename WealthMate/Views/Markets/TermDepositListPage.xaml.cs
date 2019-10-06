using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositListPage
    {
        private SearchBar _searchBar;
        public OwnedAsset OwnedAsset { get; set; }
        public TermDeposit TermDeposit { get; set; }

        public TermDepositListPage()
        {
            LoadTermDeposits();
            InitializeComponent();
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

        ////Handles a term deposit being clicked from the term deposit page
        //private void TermDepositClicked(object sender, ItemTappedEventArgs e)
        //{
        //    TermDeposit = (TermDeposit)e.ItemData;
        //    AddTDForm.IsOpen = true;

        //}

        ////Handles when the "Add" button inside the pop-up is clicked to initiate adding the term deposit to the users profile
        //private void AddInPopupClicked(object sender, System.EventArgs e)
        //{
        //    OwnedAsset = new OwnedAsset(TermDeposit.Provider, System.DateTime.Now, "Term Deposit", 0f, TermDeposit.InterestRate, TermDeposit.LengthInMonths, 0, 0f, 0f);

        //    if (OwnedAsset.PrincipalValue == 0)
        //    {
        //        NullValueErrorPopup.IsOpen = true;
        //    }
        //    OwnedAsset.UpdateOwnedAsset();
        //    ((App)Application.Current).User.Portfolio.OwnedAssets.Add(OwnedAsset);
        //    AddTDForm.IsOpen = false;
        //}

        ////Handles the "Amount Purchased" field in the term deposit popup being changed by the user
        //private void Handle_AmountChanged(object sender, ValueEventArgs e)
        //{
        //    float.TryParse(e.Value.ToString(), out var value);
        //    OwnedAsset.PrincipalValue = value;
        //}
    }
}