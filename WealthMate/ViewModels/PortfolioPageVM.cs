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
        private PieData _termD = new PieData();
        private PieData _bond = new PieData();
        private PieData _stock = new PieData();
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
    }
}
