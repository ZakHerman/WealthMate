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
            CurrentPortfolio = (Application.Current as App).User.Portfolio;         //Captures portfolio of current user.
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
                    _termD.Quantity += asset.CurrentValue;
                    _termD.PrincipalQuantity += asset.PrincipalValue;
                }
                if (asset.Type.Equals("Bond"))
                {
                    _bond.Quantity += asset.CurrentValue;
                    _bond.PrincipalQuantity += asset.PrincipalValue;
                }
                if (asset is OwnedStock)
                {
                    _stock.Quantity += asset.CurrentValue;
                    _stock.PrincipalQuantity += asset.PrincipalValue;
                }
                // insert more code for any other possible types      
            }
            _termD.AssetType = "Term Deposits";
            _bond.AssetType = "Bonds";
            _stock.AssetType = "Stocks";

            _termD.ReturnPercentage = ((_termD.Quantity - _termD.PrincipalQuantity) / _termD.PrincipalQuantity) * 100;
            _bond.ReturnPercentage = ((_bond.Quantity - _bond.PrincipalQuantity) / _bond.PrincipalQuantity) * 100;
            _stock.ReturnPercentage = ((_stock.Quantity - _stock.PrincipalQuantity) / _stock.PrincipalQuantity) * 100;

            _bond.PositiveChecker();            //XAML Flag to see if label should be red or green (negative/positive returns)
            _termD.PositiveChecker();
            _stock.PositiveChecker();

            pieChart.Add(_bond);
            pieChart.Add(_termD);
            pieChart.Add(_stock);
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
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