using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class PortfolioPageVM 
    {
        public Portfolio CurrentPortfolio { get; set; }
        public ObservableCollection<OwnedAsset> OwnedAssets { get; set; }
        public ObservableCollection<PieData> pieChart { get; set; }
        private PieData _termD = new PieData("Term Deposits");              //Pie data for each section/category of pie chart
        private PieData _bond = new PieData("Bonds");
        private PieData _stock = new PieData("Stocks");
        public List<PieData> pieList { get; set; }

        public PortfolioPageVM()
        {
            CurrentPortfolio = (Application.Current as App).User.Portfolio;         //Captures portfolio of current user.
            OwnedAssets = CurrentPortfolio.OwnedAssets;
            pieChart = new ObservableCollection<PieData>();
            SetData(1);
            pieList = GetPiesData().OrderBy(t => t.Value).ToList();
        }

        // list to display in picker menu, each item has a key and value
        public List<PieData> GetPiesData()
        {
            var pieData = new List<PieData>()
            {
                new PieData(){Key = 1, Value = "All Assets"},
                new PieData(){Key = 2, Value = "Term Deposit"},
                new PieData(){Key = 3, Value = "Stock"}
            };

            return pieData;
        }

        // to get the selected pie from the picker menu in the view
        private PieData _selectedPie { get; set; }
        public PieData SelectedPie
        {
            get { return _selectedPie; }
            set
            {
                if(_selectedPie != value)
                {
                    _selectedPie = value;

                    SetData(_selectedPie.Key);
                }
            }
        }

        //method takes key of pieList and uses switch to change to respective pie chart
        public void SetData(int picker)
        {
            switch(picker)
            {
                case 1:
                    pieChart.Clear();

                    foreach (OwnedAsset asset in CurrentPortfolio.OwnedAssets)
                    {
                        asset.UpdateOwnedAsset();

                        if (asset.Type.Equals("Term Deposit"))
                            _termD.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                        else if (asset.Type.Equals("Bond"))
                            _bond.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                        else if (asset is OwnedStock)
                            _stock.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                        // insert more code for any other possible types      
                    }

                    _termD.CalculateReturnPercentage();
                    _bond.CalculateReturnPercentage();
                    _stock.CalculateReturnPercentage();

                    _bond.PositiveChecker();            //XAML Flag to see if label should be red or green (negative/positive returns)
                    _termD.PositiveChecker();
                    _stock.PositiveChecker();

                    pieChart.Add(_bond);
                    pieChart.Add(_termD);
                    pieChart.Add(_stock);

                    break;
                case 2:
                    pieChart.Clear();
                    foreach (OwnedAsset asset in CurrentPortfolio.OwnedAssets)
                    {
                        if (asset.Type.Equals("Term Deposit"))
                        {
                            asset.UpdateOwnedAsset();
                            PieData termD = new PieData(asset.AssetName);
                            termD.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                            termD.CalculateReturnPercentage();
                            termD.PositiveChecker();
                            pieChart.Add(termD);
                        }
                    }
                    break;
                case 3:
                    pieChart.Clear();
                    foreach (OwnedAsset asset in CurrentPortfolio.OwnedAssets)
                    {
                        if (asset is OwnedStock)
                        {
                            asset.UpdateOwnedAsset();
                            PieData stock = new PieData(asset.AssetName);
                            stock.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                            stock.CalculateReturnPercentage();
                            stock.PositiveChecker();
                            pieChart.Add(stock);
                        }
                    }
                    break;
            }
        }
    }
    //Button captures still need to be implemented
}

