using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class PortfolioTest
    {
        private Portfolio testPortfolio;
        private OwnedAsset testAsset;
        private OwnedAsset testAsset2;


        
        public PortfolioTest()
        {
           testPortfolio = new Portfolio();
           testAsset = new OwnedAsset("Test1", "29/08/19", "Term Deposit", 1000, 0.10f, 5, 2, 0);
           testAsset2 = new OwnedAsset("Test2", "30/08/19", "Bond", 1000, 0.04f, 3, 1, 40);
        }

        [TestMethod]
        public void TestAddAsset()
        {
            testPortfolio.AddAsset(testAsset);
            Assert.IsTrue(testPortfolio.OwnedAssets.Contains(testAsset));
        }

        public void TestRemoveAsset()
        {
            testPortfolio.AddAsset(testAsset);
            testPortfolio.AddAsset(testAsset2);
            testPortfolio.RemoveAsset(testAsset);
            Assert.IsFalse(testPortfolio.OwnedAssets.Contains(testAsset));
        }

        public void testPortfolioTotalValue()
        {
            //Devin
        }

        public void testPortfolioTotalReturn()
        {
            //Zak
        }

        public void testPortfolioTotalReturnRate()
        {
            //Zak
        }

        public void testPortfolioDayReturn()
        {
            //Zak
        }

        public void testPortfolioDayReturnRate()
        {
            //Zak
        }


    }
}
