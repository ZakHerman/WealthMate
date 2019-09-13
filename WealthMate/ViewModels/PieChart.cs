using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WealthMate.Models;

namespace WealthMate.ViewModels
{
    public class PieChart
    {
        public ObservableCollection<PieData> pieChart { get; set; }
        //get portfolio (creating a new one and adding assets until data exists)
        public Portfolio portfolio = new Portfolio();
        //insert more instances of PieData for each possible type of asset
        private PieData _termD = new PieData();
        private PieData _bond = new PieData();
        private PieData _stock = new PieData();

        public PieChart()
        {
            pieChart = new ObservableCollection<PieData>();
            PortfolioAdd();
            SetData();
        }

        //temporary method while not portfolio data exists.
        public void PortfolioAdd()
        {
            portfolio.AddAsset(new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0));
            portfolio.AddAsset(new OwnedAsset("Westpac", new System.DateTime(2019, 09, 09, 0, 0, 0), "Term Deposit", 12f, 1.2f, 12, 1, 3));
            portfolio.AddAsset(new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3));
            portfolio.AddAsset(new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40));
            portfolio.AddAsset(new OwnedStock(new Stock("Burger Fuel", 42.2f,new System.DateTime(2019, 09, 09, 0, 0, 0), 4, 4), new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100.0f));
        }

        //method sorts assets within portfolio into types and assigns a total value 
        public void SetData()
        {
            foreach (OwnedAsset asset in portfolio.OwnedAssets)
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
                    this._stock.Quantity += 600; //asset.CurrentValue; NOT WORKING CURRENTLY
                }
                // insert more code for any other possible types      
            }

            pieChart.Add(_bond);
            pieChart.Add(_termD);
            pieChart.Add(_stock);
        }
    }
}
