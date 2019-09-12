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

        public PieChart()
        {
            pieChart = new ObservableCollection<FakeAsset>();
            FillTestData();
        }


        private void FillTestData()
        {
            FakeAsset testAsset = new FakeAsset();
            FakeAsset testAsset2 = new FakeAsset();

            testAsset.StockName = "A2 Milk";
            testAsset2.StockName = "Fonterra";

            testAsset.Quantity = 10;
            testAsset2.Quantity = 90;


            pieChart.Add(testAsset);
            pieChart.Add(testAsset2);
        }

    }
}
