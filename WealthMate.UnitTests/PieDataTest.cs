using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class PieDataTest
    {
        private PieData pieData1;
        private PieData pieData2;
        private PieData pieData3;
        private ObservableCollection<PieData> pieChartTest;

        [TestMethod]
        public void TestReturnPercentages()
        {
            PieData _termD = new PieData("Term Deposits");
            _termD.UpdateValues(100, 30);
            _termD.UpdateValues(2300, 1000);
            PieData _bond = new PieData("Bonds");
            _bond.UpdateValues(1400, 1000);
            PieData _stock = new PieData("Stocks");
            _stock.UpdateValues(5000, 3400);
            _stock.UpdateValues(1000, 6000);

            _termD.CalculateReturnPercentage();
            _bond.CalculateReturnPercentage();
            _stock.CalculateReturnPercentage();

            float _termDExpectedVal = 133.01f;
            float _termDcurrentVal = (float)Math.Round(_termD.ReturnPercentage, 2);

            float _bondExpectedVal = 40.00f;
            float _bondDcurrentVal = (float)Math.Round(_bond.ReturnPercentage, 2);

            float _stockExpectedVal = -36.17f;
            float _stockcurrentVal = (float)Math.Round(_stock.ReturnPercentage, 2);

            Assert.AreEqual(_termDExpectedVal, _termDcurrentVal);
            Assert.AreEqual(_bondExpectedVal, _bondDcurrentVal);
            Assert.AreEqual(_stockExpectedVal, _stockcurrentVal);
        }

        [TestMethod]
        public void TestUpdateValues()
        {
            PieData pieData = new PieData("Term Deposit");

            pieData.Quantity = 0.0f;
            pieData.PrincipalQuantity = 0.0f;

            float newCurrentValue = 250.60f;
            float newPrincipalValue = 50.23f;

            pieData.UpdateValues(newCurrentValue, newPrincipalValue);

            Assert.AreEqual(pieData.Quantity, newCurrentValue);
            Assert.AreEqual(pieData.PrincipalQuantity, newPrincipalValue);
        }

        // mimicked from the the PortfolioPageVM 
        [TestMethod]
        public void TestPieChanger()
        {
            pieChartTest = new ObservableCollection<PieData>();
            pieData1 = new PieData() { Key = 1, Value = "All Assets" };
            pieData2 = new PieData() { Key = 2, Value = "Term Deposit" };
            pieData3 = new PieData() { Key = 3, Value = "Stock" };

            Random rand = new Random();
            int userSelectsFromPicker = rand.Next(1, 3);

            switch(userSelectsFromPicker)
            {
                case 1:
                    pieChartTest.Add(pieData1);
                    Assert.IsTrue(pieChartTest.Contains(pieData1));
                    break;
                case 2:
                    pieChartTest.Add(pieData2);
                    Assert.IsTrue(pieChartTest.Contains(pieData2));
                    break;
                case 3:
                    pieChartTest.Add(pieData3);
                    Assert.IsTrue(pieChartTest.Contains(pieData3));
                    break;
            }
        }
    }
}
