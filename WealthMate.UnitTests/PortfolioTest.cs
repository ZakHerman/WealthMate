using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;

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

        public PortfolioTest()
        {
            testAsset = new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0);
            testAsset2 = new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40);
            testAsset3 = new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3);
            testAsset4 = new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40);
            testAsset5 = new OwnedStock(new Stock { CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4 }, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100.0f);

            testPortfolio = new Portfolio();
            testPortfolio.AddAsset(testAsset);
            testPortfolio.AddAsset(testAsset2);

            testPortfolio2 = new Portfolio();
            testPortfolio2.AddAsset(testAsset3);

            testPortfolio3 = new Portfolio();
            testPortfolio3.AddAsset(testAsset4);
            testPortfolio3.AddAsset(testAsset5);
        }

        [TestMethod]
        public void TestAddAsset()
        {
            testPortfolio.AddAsset(testAsset);
            Assert.IsTrue(testPortfolio.OwnedAssets.Contains(testAsset));
        }

        [TestMethod]
        public void TestRemoveAsset()
        {
            testPortfolio.AddAsset(testAsset);
            testPortfolio.AddAsset(testAsset2);
            testPortfolio.RemoveAsset(testAsset);
            Assert.IsFalse(testPortfolio.OwnedAssets.Contains(testAsset));
        }
        [TestMethod]
        public void testPortfolioTotalValue()
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
    }
}
