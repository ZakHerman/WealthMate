using System.Collections.Generic;
using System.Linq;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.ComboBox;
using WealthMate.Models;
using WealthMate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioListPage
    {
        private SearchBar _searchBar;

        public PortfolioListPage()
        {
            InitializeComponent();
        }

        // Search bar functionality
        public void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (PortfolioList.DataSource != null)
            {
                PortfolioList.DataSource.Filter = FilterAssets;
                PortfolioList.DataSource.RefreshFilter();
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

        // Sorts StockList list according to picker value upon picker index value changing
        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = BindingContext as PortfolioViewModel;
            var index = ((SfComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderBy(asset => asset.AssetName);
                    break;
                case 1:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderByDescending(asset => asset.AssetName);
                    break;
                case 2:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderBy(asset => asset.TotalReturn);
                    break;
                case 3:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderByDescending(asset => asset.TotalReturn);
                    break;
                case 4:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderBy(asset => asset.CurrentValue);
                    break;
                case 5:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderByDescending(asset => asset.CurrentValue);
                    break;
                case 6:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderBy(asset => asset.PurchaseDate);
                    break;
                case 7:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderByDescending(asset => asset.PurchaseDate);
                    break;
                case 8:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderBy(asset => asset.TotalReturnRate);
                    break;
                case 9:
                    PortfolioList.ItemsSource = vm?.OwnedAssets.OrderByDescending(asset => asset.TotalReturnRate);
                    break;
            }
        }
    }
}