using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class PieDataTest
    {
        [TestMethod]
        public void TestReturnPercentages()
        {
            PieData _termD = new PieData("Term Deposits");
            _termD.UpdateValues(100, 30);
            _termD.UpdateValues(2300, 1000);
            PieData _bond = new PieData("Bonds");
            _bond.UpdateValues(1400, 1000);
            PieData _stock = new PieData("Stocks");
            _stock.UpdateValues(5000, 3400);
            _stock.UpdateValues(1000, 6000);

            _termD.CalculateReturnPercentage();
            _bond.CalculateReturnPercentage();
            _stock.CalculateReturnPercentage();

            float _termDExpectedVal = 133.01f;
            float _termDcurrentVal = (float)Math.Round(_termD.ReturnPercentage, 2);

            float _bondExpectedVal = 40.00f;
            float _bondDcurrentVal = (float)Math.Round(_bond.ReturnPercentage, 2);

            float _stockExpectedVal = -36.17f;
            float _stockcurrentVal = (float)Math.Round(_stock.ReturnPercentage, 2);

            Assert.AreEqual(_termDExpectedVal, _termDcurrentVal);
            Assert.AreEqual(_bondExpectedVal, _bondDcurrentVal);
            Assert.AreEqual(_stockExpectedVal, _stockcurrentVal);
        }
    }
}
