using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class OwnedStockTest
    {
        private OwnedStock test1;
        private OwnedStock test2;
        private OwnedStock test3;
        private OwnedStock test4;

        public OwnedStockTest()
        {
            test1 = new OwnedStock();
            test2 = new OwnedStock(new Stock { CompanyName = "Burger Fuel", CurrentPrice = 42.2f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 42.4f }, new System.DateTime(2019, 09, 09, 0, 0, 0), 50.0f, 100, 1000f);
            test3 = new OwnedStock(new Stock { CompanyName = "Spark", CurrentPrice = 3.24f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 3.20f }, new System.DateTime(2019, 09, 09, 0, 0, 0), 3.00f, 150, 500f);
            test4 = new OwnedStock(new Stock { CompanyName = "A2 Milk", CurrentPrice = 13.23f, LastTrade = new DateTime(2019, 09, 09, 0, 0, 0), Shares = 4, Volume = 4, PriceClose = 13.15f }, new System.DateTime(2019, 09, 09, 0, 0, 0), 14.0f, 300, 1000f);
        }

        [TestMethod]
        public void TestReturnGoalProgress() //Note this changes every day
        {
            float delta = 0.1f;

            float expectedVal = -78f;
            float returnGoalProgress = test2.ReturnGoalProgress;

            float expectedVal2 = 7.2f;
            float returnGoalProgress2 = test3.ReturnGoalProgress;

            float expectedVal3 = -23.10f;
            float returnGoalProgress3 = test4.ReturnGoalProgress;

            Assert.AreEqual(expectedVal, returnGoalProgress, delta);
            Assert.AreEqual(expectedVal2, returnGoalProgress2, delta);
            Assert.AreEqual(expectedVal3, returnGoalProgress3, delta);
        }

        [TestMethod]
        public void TestEditStock()
        {
            test1.SharesPurchased = 30;
            test1.PurchasedPrice = 32.4f;

            int newShares = 60;
            float newPrice = 28.5f;
            float returnGoal = 400f;

            test1.EditStock(newShares, newPrice, returnGoal);

            Assert.AreEqual(test1.SharesPurchased, newShares);
            Assert.AreEqual(test1.PurchasedPrice, newPrice);
        }

    }
}
