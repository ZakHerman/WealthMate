using Syncfusion.ListView.XForms;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WealthMate.Services;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockListPage
    {
        private SearchBar _searchBar;

        public StockListPage()
        {
            LoadStocks();

            /*foreach (var s in Api.Stocks)
            {
                s.UpdateStock();
            }*/

            InitializeComponent();
        }

        private async void LoadStocks()
        {
            Api.Stocks = await Api.GetStocksAsync();
            StockList.ItemsSource = Api.Stocks;
        }

        // Event handler for watchlist stock being pressed
        private async void StockListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        //search functionality below
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (StockList.DataSource != null)
            {
                StockList.DataSource.Filter = FilterStocks;
                StockList.DataSource.RefreshFilter();
            }
        }

        private bool FilterStocks(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

            return obj is Stock stock && (stock.CompanyName.ToLower().Contains(_searchBar.Text.ToLower())
                                          || stock.Symbol.ToLower().Contains(_searchBar.Text.ToLower()));
        }
    }
}