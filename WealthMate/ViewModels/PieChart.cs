using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WealthMate.Models;

namespace WealthMate.ViewModels
{
    public class PieChart
    {
        public ObservableCollection<FakeAsset> pieChart { get; set; }

        //public ObservableCollection<OwnedAsset> pieChart { get; set; }

        public PieChart()
        {
            pieChart = new ObservableCollection<FakeAsset>();
            FillTestData();
        }


        private void FillTestData()
        {
            FakeAsset testAsset = new FakeAsset();
            FakeAsset testAsset2 = new FakeAsset();

            testAsset.AssetType = "Stock";
            testAsset.AssetType = "Term Deposit";

            testAsset.StockName = "A2 Milk";
            testAsset2.StockName = "Fonterra";

            testAsset.Quantity = 10;
            testAsset2.Quantity = 90;

            //testAsset.Type = "Stock";
            //testAsset2.Type = "Term Deposit";

            //testAsset.AssetName = "A2 Milk";
            //testAsset2.AssetName = "Fonterra";

            //testAsset.CurrentValue = 2500;
            //testAsset2.CurrentValue = 120;

            pieChart.Add(testAsset);
            pieChart.Add(testAsset2);
        }

    }
}
