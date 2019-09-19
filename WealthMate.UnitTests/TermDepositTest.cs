using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class TermDepositTest
    {
        TermDeposit TD1 = new TermDeposit("WestpacTest", 10, 15, 12, 20.5f);
        TermDeposit TD2 = new TermDeposit("SparkTest", 0, null, 18, 14.25f);


        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("WestpacTest", TD1.Provider);
            Assert.AreEqual(10, TD1.MinDeposit);
            Assert.AreEqual(15, TD1.MaxDeposit);
            Assert.AreEqual(12, TD1.LengthInMonths);
            Assert.AreEqual(20.5f, TD1.InterestRate);

            Assert.AreEqual("SparkTest", TD2.Provider);
            Assert.AreEqual(0, TD2.MinDeposit);
            Assert.AreEqual(null, TD2.MaxDeposit);
            Assert.AreEqual(18, TD2.LengthInMonths);
            Assert.AreEqual(14.25f, TD2.InterestRate);
        }
    }
}
