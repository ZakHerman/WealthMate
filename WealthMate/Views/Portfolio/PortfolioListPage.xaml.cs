using Syncfusion.ListView.XForms;
using WealthMate.Models;
using WealthMate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioListPage
    {
        private SearchBar _searchBar;

        public PortfolioListPage()
        {
            InitializeComponent();
            BindingContext = new PortfolioViewModel();
        }

        // Search bar functionality
        public void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (List.DataSource != null)
            {
                List.DataSource.Filter = FilterAssets;
                List.DataSource.RefreshFilter();
            }
        }

        // Filtering the list view as text changes within the search bar
        private bool FilterAssets(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }

            return obj is OwnedAsset asset && (asset.AssetName.ToLower().Contains(_searchBar.Text.ToLower())
                                               || asset.Type.ToLower().Contains(_searchBar.Text.ToLower()));
        }

        // Event handler for watchlist stock being pressed
        private async void PortfolioListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (OwnedAsset)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            if (selected is OwnedStock stock)
                await Navigation.PushAsync(new OwnedStockDetailsPage(stock));
            else
                await Navigation.PushAsync(new OwnedAssetDetailsPage(selected));

            ((SfListView)sender).SelectedItem = null;
        }
    }
}