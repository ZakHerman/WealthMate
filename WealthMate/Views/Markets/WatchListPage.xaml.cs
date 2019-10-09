using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using WealthMate.ViewModels;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistPage
    {
        private SearchBar _searchBar;

        public WatchlistPage()
        {
            //BindingContext = new WatchListPageVM();
            InitializeComponent();
        }

        // Event handler for watchlist stock being pressed
        private async void WatchlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar); //set sender to SearchBar

            if (Watchlist.DataSource != null)
            {
                Watchlist.DataSource.Filter = FilterWList; //filters the data source
                Watchlist.DataSource.RefreshFilter(); // refreshes the view
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> object representing a search return
        /// <returns></returns> boolean value for checking for text in the serach bar
        private bool FilterWList(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }

            return obj is Stock stock && (stock.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || stock.Symbol.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}