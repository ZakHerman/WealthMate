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
        //search functionality below
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (SavingsList.DataSource != null)
            {
                SavingsList.DataSource.Filter = FilterStocks;
                SavingsList.DataSource.RefreshFilter();
            }
        }

        private bool FilterStocks(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

            return obj is SavingsAccount savings && (savings.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || savings.AccountType.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}