using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavingsListPage
    {
        private SearchBar _searchBar;

        public SavingsListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);//set sender to SearchBar

            if (SavingsList.DataSource != null)
            {
                SavingsList.DataSource.Filter = FilterStocks; //filters the data source
                SavingsList.DataSource.RefreshFilter(); // refreshes the view
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> object representing a search return
        /// <returns></returns> boolean value for checking for text in the serach bar
        private bool FilterStocks(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }
            else
            {
                return obj is SavingsAccount savings && (savings.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                              || savings.AccountType.ToLower().Contains(_searchBar.Text.ToLower()));
            }
        }
    }
}