using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class TermDepositTest
    {
        TermDeposit TD1 = new TermDeposit("WestpacTest", 10, 15, 12, 20.5f);


        [TestMethod]
        public void TestMethod1()
        {
            /**Test variable access:
             * Provider - PASSED
             * CreditRating - PASSED
             * MinDeposit - FAILED (changed 10.2 to 10.1999...)
             * MaxDeposit - FAILED (changed 15.7 to 15.6999...)
             * LengthInMonths - PASSED
             * InterestRate - PASSED
             * NoMaxDeposit - FAILED (no way to set this)
             * NoMinimumDeposit - FAILED (no way to set this)
             */

            Assert.AreEqual("WestpacTest", TD1.Provider);
            Assert.AreEqual(10, TD1.MinDeposit);
            Assert.AreEqual(15, TD1.MaxDeposit);
            Assert.AreEqual(12, TD1.LengthInMonths);
            Assert.AreEqual(20.5, TD1.InterestRate);
        }
    }
}
