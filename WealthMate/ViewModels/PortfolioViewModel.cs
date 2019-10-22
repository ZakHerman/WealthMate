using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class PortfolioViewModel
    {
        public Portfolio CurrentPortfolio { get; set; }
        public ObservableCollection<OwnedAsset> OwnedAssets { get; set; }
        public ObservableCollection<PieData> pieChart { get; set; }
        private PieData _termD = new PieData("Term Deposits"); //Pie data for each section/category of pie chart
        private PieData _stock = new PieData("Stocks");
        public List<PieData> pieList { get; set; }

        public PortfolioViewModel()
        {
            CurrentPortfolio = ((App) Application.Current).User.Portfolio; // Captures portfolio of current user.
            OwnedAssets = CurrentPortfolio.OwnedAssets;
            pieChart = new ObservableCollection<PieData>();

            SetData(1);

            pieList = GetPiesData().OrderBy(t => t.Value).ToList();
        }

        // List to display in picker menu, each item has a key and value
        public List<PieData> GetPiesData()
        {
            // Gathers data of pie chart
            var pieData = new List<PieData>
            {
                new PieData {Key = 1, Value = "All Assets"},
                new PieData {Key = 2, Value = "Term Deposit"},
                new PieData {Key = 3, Value = "Stock"}
            };

            return pieData;
        }

        // To get the selected pie from the picker menu in the view
        private PieData _selectedPie;
        public PieData SelectedPie
        {
            get => _selectedPie;
            set
            {
                if(_selectedPie != value)
                {
                    _selectedPie = value;

                    SetData(_selectedPie.Key);
                }
            }
        }

        // Takes key of pieList and uses switch to change to respective pie chart
        public void SetData(int picker)
        {
            switch(picker)                                                              
            {
                case 1:
                    pieChart.Clear();

                    foreach (var asset in CurrentPortfolio.OwnedAssets)
                    {
                        asset.UpdateOwnedAsset();

                        if (asset.Type.Equals("Term Deposit"))
                            _termD.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                        else if (asset is OwnedStock)
                            _stock.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                    }

                    _termD.CalculateReturnPercentage();
                    _stock.CalculateReturnPercentage();

                     // XAML Flag to see if label should be red or green (negative/positive returns)
                    _termD.PositiveChecker();
                    _stock.PositiveChecker();

                    // Adds asset categories to Pie chart
                    pieChart.Add(_termD);
                    pieChart.Add(_stock);

                    break;
                case 2:
                    pieChart.Clear();
                    foreach (var asset in CurrentPortfolio.OwnedAssets)
                    {
                        if (asset.Type.Equals("Term Deposit"))
                        {
                            asset.UpdateOwnedAsset();
                            var termD = new PieData(asset.AssetName);

                            termD.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                            termD.CalculateReturnPercentage();
                            termD.PositiveChecker();

                            pieChart.Add(termD);
                        }
                    }
                    break;
                case 3:
                    pieChart.Clear();
                    foreach (var asset in CurrentPortfolio.OwnedAssets)
                    {
                        if (asset is OwnedStock)
                        {
                            asset.UpdateOwnedAsset();
                            var stock = new PieData(asset.AssetName);

                            stock.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                            stock.CalculateReturnPercentage();
                            stock.PositiveChecker();

                            pieChart.Add(stock);
                        }
                    }
                    break;
            }
        }

        // attribute for observing the selected item in the picker
        // calls SetList() which sorts list according to picker value
        private string _selectedCriteria;
        public string SelectedCriteria
        {
            get => _selectedCriteria;
            set
            {
                if (_selectedCriteria != value)
                {
                    _selectedCriteria = value;

                    SetList(_selectedCriteria);
                }
            }
        }

        // Sorts OwnedAssets list according to picker value
        private void SetList(string picker)
        {
            switch (picker)
            {
                case "Company Name":
                    SortList(OwnedAssets.OrderBy(asset => asset.AssetName));
                    break;
                case "Current Value (high-low)":
                    SortList(OwnedAssets.OrderByDescending(asset => asset.CurrentValue));
                    break;
                case "Current Value (low-high)":
                    SortList(OwnedAssets.OrderBy(asset => asset.CurrentValue));
                    break;
                case "Day Return Rate":
                    SortList(OwnedAssets.OrderByDescending(asset => asset.TotalReturn));
                    break;
                case "Purchase Date (group by type)":
                    SortList(OwnedAssets.OrderByDescending(asset => asset.Length));
                    break;
                case "Total Return Rate":
                    SortList(OwnedAssets.OrderByDescending(asset => asset.TotalReturnRate));
                    break;
            }
        }

        // Clears OwnedAssets list and re-adds assets to collection
        // This is required due to the return type of OrderBy and OrderByDescending methods
        private void SortList(IEnumerable<OwnedAsset> linqResults)
        {
            var observableC = new ObservableCollection<OwnedAsset>(linqResults);
            OwnedAssets.Clear();
            foreach (var asset in observableC)
            {
                OwnedAssets.Add(asset);
            }
        }
    }
}