using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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
        
        /*        public event PropertyChangedEventHandler PropertyChanged;
                protected void OnPropertyChanged([CallerMemberName] string name = null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
                }*/

        public void PieChartChanger_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            SetData(selectedIndex);
        }

        public PortfolioPageVM()
        {
            CurrentPortfolio = (Application.Current as App).User.Portfolio;         //Captures portfolio of current user.
            OwnedAssets = CurrentPortfolio.OwnedAssets;
            pieChart = new ObservableCollection<PieData>();
            SetData(1);
        }
        //method sorts assets within portfolio into types and assigns a total value 
        public void SetData(int picker)
        {
            //defualt or if all assets is chosen in picker 
            if (picker == 0 || picker == -1)
            {
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
            }
            //if Stocks selected
            if (picker == 2)
            {
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
            }
            //if Term selected
            if (picker == 1)
            {
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
            }
        }
        }

        //Button captures still need to be implemented
    }

