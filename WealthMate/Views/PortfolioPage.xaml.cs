using System.ComponentModel;
using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using Syncfusion.ListView.XForms;
using WealthMate.ViewModels;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class PortfolioPage
    {
        private SearchBar _searchBar;

        public PortfolioPage()
        {
            BindingContext = new PortfolioPageVM();
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
            );
        }

       

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        public void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar); //set sender to SearchBar

            if (List.DataSource != null)
            {
                List.DataSource.Filter = FilterAssets; //filters the data source
                List.DataSource.RefreshFilter(); // refreshes the view
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> object representing a search return
        /// <returns></returns> boolean value for checking for text in the serach bar
        private bool FilterAssets(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }
            else
            {
                return obj is OwnedAsset asset && (asset.AssetName.ToLower().Contains(_searchBar.Text.ToLower())
                              || asset.Type.ToLower().Contains(_searchBar.Text.ToLower()));
            }
        }

        // Event handler for watchlist stock being pressed
        private async void PortfolioListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = (OwnedAsset)e.ItemData;

            if (selected == null)
                return;

            // Push stockdetailspage on top of stack
            if(selected is OwnedStock)
                await Navigation.PushAsync(new OwnedStockDetailsPage((OwnedStock)selected));
            else
                await Navigation.PushAsync(new OwnedAssetDetailsPage(selected));

            ((SfListView)sender).SelectedItem = null;
        }
    }
}