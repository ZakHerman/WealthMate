using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class UserTest
    {
        private User testUser;
        public UserTest()
        {
            testUser = new User();
        }

        [TestMethod]
        public void TestAddStock()
        {
            Stock testStock = new Stock();
            Stock testStock2 = new Stock();
            Stock testStock3 = new Stock();

            testUser.WatchListStocks.Add(testStock);
            testUser.WatchListStocks.Add(testStock2);
            testUser.WatchListStocks.Add(testStock3);

            Assert.IsTrue(testUser.WatchListStocks.Contains(testStock));
            Assert.IsTrue(testUser.WatchListStocks.Contains(testStock2));
            Assert.IsTrue(testUser.WatchListStocks.Contains(testStock3));
        }

        [TestMethod]
        public void TestRemoveStock()
        {
            Stock testStock = new Stock();
            Stock testStock2 = new Stock();
            Stock testStock3 = new Stock();

            testUser.WatchListStocks.Add(testStock);
            testUser.WatchListStocks.Add(testStock2);
            testUser.WatchListStocks.Add(testStock3);

            testUser.WatchListStocks.Remove(testStock);
            testUser.WatchListStocks.Remove(testStock2);
            testUser.WatchListStocks.Remove(testStock3);

            Assert.IsFalse(testUser.WatchListStocks.Contains(testStock));
            Assert.IsFalse(testUser.WatchListStocks.Contains(testStock2));
            Assert.IsFalse(testUser.WatchListStocks.Contains(testStock3));
        }
    }
}
