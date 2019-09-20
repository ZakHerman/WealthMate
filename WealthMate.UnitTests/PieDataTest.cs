using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;

namespace WealthMate.UnitTests
{
    [TestClass]
    class PieDataTest
    {
        private OwnedAsset testAsset;
        private OwnedAsset testAsset2;
        private OwnedAsset testAsset3;
        private User testUser;

        public PieDataTest()
        {
            testAsset = new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0);
            testAsset2 = new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40);
            testAsset3 = new OwnedAsset("Test3", new DateTime(2018, 2, 14), "Stock", 1000, 0.8f, 4, 3, 20);
            testUser = new User();

        }

        [TestMethod]
        public void TestPieDataValues()
        {
            testUser.Portfolio.OwnedAssets.Add(testAsset);
            testUser.Portfolio.OwnedAssets.Add(testAsset2);
            testUser.Portfolio.OwnedAssets.Add(testAsset3);

            //Assert.AreEqual();

        }

        [TestMethod]
        public void TestReturnPercentages()
        {
            OwnedAsset testOwnedAsset = new OwnedAsset("Test1", new DateTime(2019, 9, 5), "Term Deposit", 1000, 0.10f, 5, 2, 0);
            OwnedAsset testOwnedAsset2 = new OwnedAsset("Westpac", new System.DateTime(2019, 09, 09, 0, 0, 0), "Term Deposit", 12f, 1.2f, 12, 1, 3);
            OwnedAsset testOwnedAsset3 = new OwnedAsset("Air New Zealand", new System.DateTime(2019, 09, 06, 0, 0, 0), "Term Deposit", 12f, 2.4f, 9, 1, 3);
            OwnedAsset testOwnedAsset4 = new OwnedAsset("Test2", new DateTime(2016, 1, 14), "Bond", 1000, 0.04f, 3, 1, 40);
            OwnedAsset testOwnedAsset5 = new OwnedStock(new Stock { CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4 }, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100.0f);
            testUser.Portfolio.OwnedAssets.Add(testAsset);
            testUser.Portfolio.OwnedAssets.Add(testAsset2);
            testUser.Portfolio.OwnedAssets.Add(testAsset3);

            //Assert.AreEqual();

        }
    }
}
