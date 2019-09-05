using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;

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
            test1 = new OwnedAsset("ANZ Bank", "18/03/18", "Term Deposit", 10000, 0.27f, 12, 4, 0);
            test2 = new OwnedAsset("ASB Bank", "08/01/19", "Term Deposit", 2000, 0.30f, 6, 4, 0);
            test3 = new OwnedAsset("TSB Bank", "29/02/17", "Term Deposit", 1000, 0.20f, 3, 4, 0);
        }

        [TestMethod]
        public void TestCurrentValue()
        {
            float expectedVal = 0.0f;
            float currentVal = test1.CurrentValue;

            Assert.AreEqual(expectedVal, currentVal);
        }

        [TestMethod]
        public void TestTotalReturn()
        {
            float expectedVal = 0.0f;
            float totalRet = test2.TotalReturn;

            Assert.AreEqual(expectedVal, totalRet);
        }

        [TestMethod]
        public void TestTotalReturnRate()
        {
            float expectedRate = 0.0f;
            float totalRetRate = test3.TotalReturnRate;

            Assert.AreEqual(expectedRate, totalRetRate);
        }


    }
}
