using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class OwnedAssetTest
    {   
        private OwnedAsset test1;
        private OwnedAsset test2;
        private OwnedAsset test3;

        public OwnedAssetTest()
        {
            test1 = new OwnedAsset("ANZ Bank", new DateTime(2019, 9, 3), "Term Deposit", 10000, 0.27f, 12, 4, 0);
            test2 = new OwnedAsset("ASB Bank", new DateTime(2019, 1, 8), "Term Deposit", 2000, 0.30f, 6, 4, 0);
            test3 = new OwnedAsset("TSB Bank", new DateTime(2017, 2, 21), "Term Deposit", 1000, 0.20f, 3, 4, 0);
        }

        [TestMethod]
        public void TestCurrentValue() //Note this changes every day
        {
            float delta = 0.1f;

            float expectedVal = 10122.35f;
            float currentVal = test1.CurrentValue;

            float expectedVal2 = 2447.60f;
            float currentVal2 = test2.CurrentValue;

            float expectedVal3 = 1653.34f;
            float currentVal3 = test3.CurrentValue;

            Assert.AreEqual(expectedVal, currentVal, delta);
            Assert.AreEqual(expectedVal2, currentVal2, delta);
            Assert.AreEqual(expectedVal3, currentVal3, delta);
        }

        [TestMethod]
        public void TestTotalReturn() //Note this changes every day
        {
            float delta = 0.1f;

            float expectedVal = 122.35f;
            float totalRet = test1.TotalReturn;

            float expectedVal2 = 447.61f;
            float totalRet2 = test2.TotalReturn;

            float expectedVal3 = 653.34f;
            float totalRet3 = test3.TotalReturn;

            Assert.AreEqual(expectedVal, totalRet, delta);
            Assert.AreEqual(expectedVal2, totalRet2, delta);
            Assert.AreEqual(expectedVal3, totalRet3, delta);
        }

        [TestMethod]
        public void TestTotalReturnRate()
        {
            float delta = 0.1f;

            float expectedVal = 1.22f;
            float totalRetR = test1.TotalReturnRate;

            float expectedVal2 = 22.38f;
            float totalRetR2 = test2.TotalReturnRate;

            float expectedVal3 = 65.33f;
            float totalRetR3 = test3.TotalReturnRate;

            Assert.AreEqual(expectedVal, totalRetR, delta);
            Assert.AreEqual(expectedVal2, totalRetR2, delta);
            Assert.AreEqual(expectedVal3, totalRetR3, delta);
        }

        [TestMethod]
        public void TestEditOwnedAsset()
        {
            float newInterestRate = 1.2f;
            int newLength = 9;
            float newRegPayments = 3.2f;

            test1.EditAsset(newInterestRate, newLength, newRegPayments, test1);

            OwnedAsset t1 = new OwnedAsset("ANZ Bank", new DateTime(2019, 9, 3), "Term Deposit", 10000, 1.2f, 9, 4, 3.2f);

            Assert.AreEqual(test1.InterestRate, t1.InterestRate);
            Assert.AreEqual(test1.Length, t1.Length);
            Assert.AreEqual(test1.RegularPayment, t1.RegularPayment);
        }


        //[TestMethod]
        //public void testTotalReturnPercent()
        //{

        //}

        //[TestMethod]
        //public void testTotalReturnAbs()
        //{

        //}


    }
}
