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

         //   Assert.AreEqual()

        }
    }
}
