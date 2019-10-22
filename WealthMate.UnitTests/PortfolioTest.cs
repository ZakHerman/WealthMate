using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class PortfolioTest
    {
        private Portfolio testPortfolio;
        private Portfolio testPortfolio2;
        private Portfolio testPortfolio3;
        private OwnedAsset testAsset;
        private OwnedAsset testAsset2;
        private OwnedAsset testAsset3;
        private OwnedAsset testAsset4;
        private OwnedStock testAsset5;
        private OwnedStock testAsset6;

        public PortfolioTest()
        {
            testPortfolio = new Portfolio();

            testPortfolio.AddAsset(new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0, 0));
            testPortfolio.AddAsset(new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40, 0));
            testPortfolio.AddAsset(new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3, 0));
            testPortfolio.AddAsset(new OwnedAsset("Test2", new DateTime(2016, 1, 15), "Bond", 1000, 0.04f, 3, 1, 40, 0));
            testPortfolio.AddAsset(new OwnedStock(new Stock { CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4 }, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100, 0));
            testPortfolio.AddAsset(new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f }, new System.DateTime(2019, 10, 09, 0, 0, 0), 3.00f, 150, 500f));

            //testPortfolio = new Portfolio();
            //testPortfolio.AddAsset(testAsset);
            //testPortfolio.AddAsset(testAsset2);

            //testPortfolio2 = new Portfolio();
            //testPortfolio2.AddAsset(testAsset3);

            //testPortfolio3 = new Portfolio();
            //testPortfolio3.AddAsset(testAsset4);
            //testPortfolio3.AddAsset(testAsset5);
        }

        [TestMethod]
        public void TestAddAsset()
        {
            //Assets were added in constructor
            Assert.IsTrue(testPortfolio.OwnedAssets.Contains(testAsset));
            Assert.IsTrue(testPortfolio2.OwnedAssets.Contains(testAsset3));
            Assert.IsTrue(testPortfolio3.OwnedAssets.Contains(testAsset5));
        }
        [TestMethod]
        public void TestAddAssetUpdatedPortfolioTotalValue()
        {
            testPortfolio.AddAsset(testAsset5);
            testPortfolio2.AddAsset(testAsset4);
            testPortfolio3.AddAsset(testAsset);

            float delta = 0.01f;

            float actual = testPortfolio.CurrentTotal;
            float expected = 6534.77f;

            float actual2 = testPortfolio2.CurrentTotal;
            float expected2 = 1323.39f;

            float actual3 = testPortfolio3.CurrentTotal;
            float expected3 = 6534.77f;

            Assert.AreEqual(expected, actual, delta, "Portfolio total value does not match");
            Assert.AreEqual(expected2, actual2, delta, "Portfolio total value does not match");
            Assert.AreEqual(expected3, actual3, delta, "Portfolio total value does not match");
        }

        [TestMethod]
        public void TestRemoveAsset()
        {
            testPortfolio.AddAsset(testAsset3); //First two assets already added in constructor

            testPortfolio.RemoveAsset(testAsset);
            testPortfolio.RemoveAsset(testAsset2);
            testPortfolio.RemoveAsset(testAsset3);

            Assert.IsFalse(testPortfolio.OwnedAssets.Contains(testAsset));
            Assert.IsFalse(testPortfolio.OwnedAssets.Contains(testAsset2));
            Assert.IsFalse(testPortfolio.OwnedAssets.Contains(testAsset3));
        }
        [TestMethod]
        public void TestRemoveAssetUpdatedPortfolioTotalValue()
        {
            testPortfolio.RemoveAsset(testAsset);
            testPortfolio2.RemoveAsset(testAsset3);
            testPortfolio3.RemoveAsset(testAsset4);

            float delta = 0.01f;     
            
            float actual = testPortfolio.CurrentTotal;
            float expected = 1310.75f;

            float actual2 = testPortfolio2.CurrentTotal;
            float expected2 = 0.00f;

            float actual3 = testPortfolio3.CurrentTotal;
            float expected3 = 4220.00f;

            Assert.AreEqual(expected, actual, delta, "Portfolio total value does not match");
            Assert.AreEqual(expected2, actual2, delta, "Portfolio total value does not match");
            Assert.AreEqual(expected3, actual3, delta, "Portfolio total value does not match");
        }

        [TestMethod]
        public void TestPortfolioTotalValue()
        {
            float delta = 0.01f;                                //Note that the value changes everyday, as calculations are adjusted to today's date.
            float actual = testPortfolio.CurrentTotal;
            float expected = 2314.77f;
            Assert.AreEqual(expected, actual, delta, "Portfolio total value does not match");
        }

        [TestMethod]
        public void TestPortfolioTotalReturn() //Note that the value changes everyday, as calculations are adjusted to today's date.
        {
            float delta = 0.01f;

            float actual = testPortfolio.TotalReturn;
            float expected = 314.77f;
            Assert.AreEqual(expected, actual, delta);

            float actual2 = testPortfolio2.TotalReturn;
            float expected2 = 0.64f;
            Assert.AreEqual(expected2, actual2, delta);

            float actual3 = testPortfolio3.TotalReturn;
            float expected3 = -469.25f;
            Assert.AreEqual(expected3, actual3, delta);
        }

        [TestMethod]
        public void TestPortfolioTotalReturnRate()
        {
            float delta = 0.01f;

            float actual = testPortfolio.TotalReturnRate;
            float expected = 15.74f;
            Assert.AreEqual(expected, actual, delta);

            float actual2 = testPortfolio2.TotalReturnRate;
            float expected2 = 5.30f;
            Assert.AreEqual(expected2, actual2, delta);

            float actual3 = testPortfolio3.TotalReturnRate;
            float expected3 = -7.82f;
            Assert.AreEqual(expected3, actual3, delta);
        }

        [TestMethod]
        public void TestPortfolioSort()
        {
            var observableC = new ObservableCollection<OwnedAsset>(testPortfolio.OwnedAssets.OrderBy(asset => asset.Length));
            
            //testPortfolio.OwnedAssets.OrderByDescending(asset => asset.CurrentValue);
            
            //testPortfolio.OwnedAssets.OrderBy(asset => asset.CurrentValue);
         
            //testPortfolio.OwnedAssets.OrderByDescending(asset => asset.TotalReturn);
              
            //testPortfolio.OwnedAssets.OrderByDescending(asset => asset.Length);
           
            //testPortfolio.OwnedAssets.OrderByDescending(asset => asset.TotalReturnRate);
                       
            foreach(OwnedAsset asset in observableC)
            {
                Console.WriteLine(asset.Length);
            }
        }

        [TestMethod]
        public void TestReturnGoalProgress() //Note this changes every day
        {
            Portfolio test1 = new Portfolio(500.0f);
            test1.AddAsset(new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0, 0));
            test1.AddAsset(new OwnedStock(new Stock { CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0),
                                          Shares = 4, Volume = 4 }, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100, 0));

            Portfolio test2 = new Portfolio(1000.0f);
            test2.AddAsset(new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40, 0));

            Portfolio test3 = new Portfolio(10.0f);
            test3.AddAsset(new OwnedAsset("Test3", new DateTime(2018, 4, 19), "Term Deposit", 1000, 0.02f, 3, 1, 0, 0));

            float delta = 0.1f;

            float expectedVal = 0.0f;
            float returnGoalProgress = test1.ReturnGoalProgress;

            float expectedVal2 = 31.87f;
            float returnGoalProgress2 = test2.ReturnGoalProgress;

            float expectedVal3 = 100.0f;
            float returnGoalProgress3 = test3.ReturnGoalProgress;

            Assert.AreEqual(expectedVal, returnGoalProgress, delta);
            Assert.AreEqual(expectedVal2, returnGoalProgress2, delta);
            Assert.AreEqual(expectedVal3, returnGoalProgress3, delta);
        }
    }
}
