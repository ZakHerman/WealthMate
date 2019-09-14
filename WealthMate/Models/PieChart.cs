using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    /// <summary>
    /// Class containing code for PieChart
    /// Currently class instantiates and populates a portfolio with test data
    /// it then instantiates objects representing the piedata 
    /// </summary>
    public class PieChart
    {
        public ObservableCollection<PieData> pieChart { get; set; }
        //get portfolio (creating a new one and adding assets until data exists)
        public Portfolio CurrentPortfolio { get; set; }
        //instances of PieData
        //insert more for each possible type of asset
        private PieData _termD = new PieData();
        private PieData _bond = new PieData();
        private PieData _stock = new PieData();

        public PieChart()
        {
            CurrentPortfolio = (Application.Current as App).User.Portfolio;
            pieChart = new ObservableCollection<PieData>();
            SetData();
        }

        /// <summary>
        /// method sorts assets within portfolio into types and assigns a total value to that type
        /// </summary>
        public void SetData()
        {
            foreach (OwnedAsset asset in CurrentPortfolio.OwnedAssets)
            {
                if (asset.Type.Equals("Term Deposit"))
                {
                    this._termD.AssetType = "Term Deposits";
                    this._termD.Quantity += asset.CurrentValue;
                }
                if (asset.Type.Equals("Bond"))
                {
                    this._bond.AssetType = "Bonds";
                    this._bond.Quantity += asset.CurrentValue;
                }
                if (asset is OwnedStock)
                {
                    this._stock.AssetType = "Stocks";
                    this._stock.Quantity += asset.CurrentValue; //asset.CurrentValue; NOT WORKING CURRENTLY
                }
                // insert more code for any other possible types      
            }

            pieChart.Add(_bond);
            pieChart.Add(_termD);
            pieChart.Add(_stock);
        }
    }
}
