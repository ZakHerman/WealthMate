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
            testAsset2.AssetType = "Term Deposit";

            testAsset.StockName = "A2 Milk";
            testAsset2.StockName = "Fonterra";

            testAsset.PercentageChange = 1.20f;
            testAsset2.PercentageChange = 8.15f;

            testAsset.Value = 8441.20f;
            testAsset2.Value = 15029.56f;

            pieChart.Add(testAsset);
            pieChart.Add(testAsset2);
        }

    }
}
