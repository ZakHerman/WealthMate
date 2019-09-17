using System.ComponentModel;
using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using Syncfusion.ListView.XForms;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class PortfolioPage
    {
        private SearchBar _searchBar;
        public float TotalValue { get; set; }
        public Portfolio CurrentPortfolio { get; set; }
        public List<OwnedAsset> OwnedAssets { get; set; }
        public ObservableCollection<PieData> pieChart { get; set; }
        private PieData _termD = new PieData();
        private PieData _bond = new PieData();
        private PieData _stock = new PieData();

        public PortfolioPage()
        {
            CurrentPortfolio = (Application.Current as App).User.Portfolio;
            OwnedAssets = CurrentPortfolio.OwnedAssets;
            pieChart = new ObservableCollection<PieData>();
            SetData();
            BindingContext = this;
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All
            );
        }

        //method sorts assets within portfolio into types and assigns a total value 
        public void SetData()
        {
            foreach (OwnedAsset asset in CurrentPortfolio.OwnedAssets)
            {
                asset.UpdateOwnedAsset();

                if (asset.Type.Equals("Term Deposit"))
                {
                    this._termD.AssetType = "Term Deposits";
                    this._termD.Quantity += asset.CurrentValue;
                    this._termD.ReturnPercentage = asset.CurrentValue / asset.TotalReturn;
                }
                if (asset.Type.Equals("Bond"))
                {
                    this._bond.AssetType = "Bonds";
                    this._bond.Quantity += asset.CurrentValue;
                    this._bond.ReturnPercentage = asset.CurrentValue / asset.TotalReturn;
                }
                if (asset is OwnedStock)
                {
                    this._stock.AssetType = "Stocks";
                    this._stock.Quantity += asset.CurrentValue;
                    this._stock.ReturnPercentage = asset.CurrentValue / asset.TotalReturn;

                }
                // insert more code for any other possible types      
            }

            pieChart.Add(_bond);
            pieChart.Add(_termD);
            pieChart.Add(_stock);
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (List.DataSource != null)
            {
                List.DataSource.Filter = FilterAssets;
                List.DataSource.RefreshFilter();
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// May need to be updated as the lists display is refined i.e. includes stocks and stock names
        /// </summary>
        /// <param name="obj"></param> 
        /// <returns></returns>
        private bool FilterAssets(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

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
            if(selected is OwnedStock)
                await Navigation.PushAsync(new OwnedStockDetailsPage((OwnedStock)selected));
            else
                await Navigation.PushAsync(new OwnedAssetDetailsPage(selected));

            ((SfListView)sender).SelectedItem = null;
        }
    }
}