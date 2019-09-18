using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositListPage
    {
        private SearchBar _searchBar;

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
            else
            {
                return obj is TermDeposit termD && (termD.Provider.ToLower().Contains(_searchBar.Text.ToLower()));
            }
        }

    }
}