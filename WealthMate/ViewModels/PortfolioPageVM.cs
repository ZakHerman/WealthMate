using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private PieData _termD = new PieData("Term Deposits");
        private PieData _bond = new PieData("Bonds");
        private PieData _stock = new PieData("Stocks");
        public PortfolioPageVM()
        {
            CurrentPortfolio = (Application.Current as App).User.Portfolio;         //Captures portfolio of current user.
            OwnedAssets = CurrentPortfolio.OwnedAssets;
            pieChart = new ObservableCollection<PieData>();
            SetData();
        }
        //method sorts assets within portfolio into types and assigns a total value 
        public void SetData()
        {
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
    }
}
