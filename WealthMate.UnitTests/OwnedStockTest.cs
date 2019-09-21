using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class OwnedStockTest
    {
        private OwnedStock test1;

        public OwnedStockTest()
        {
            test1 = new OwnedStock();
            // public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, float sharesPurchased)
        }

        [TestMethod]
        public void TestEditStock()
        {
            test1.SharesPurchased = 30;
            test1.PurchasedPrice = 32.4f;

            int newShares = 60;
            float newPrice = 28.5f;

            test1.EditStock(newShares, newPrice, test1);

            Assert.AreEqual(test1.SharesPurchased, newShares);
            Assert.AreEqual(test1.PurchasedPrice, newPrice);
        }

    }
}
