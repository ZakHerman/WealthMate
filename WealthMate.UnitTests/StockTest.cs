using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class StockTest
    {
       

        //private Stock StockA = new Stock("SparkTest",2.5f, new DateTime(2019, 09, 03, 0, 0, 0), 10, 100);
        //private Stock StockB = new Stock("SPCATest",4.9f, new DateTime(2019, 08, 30, 0, 0, 0), 20, 420);
            //public Stock(float price, DateTime priceDate, int shares, int volume)
            //Elli: concerned that this is just me creating data and getters for these... There should be actual methods for 
            //retrieving and getting updated data for existing stocks (or will stocks be created a new each time?)

        [TestMethod]
        public void TestMethod1()
        {
            //Elli: Access and view details + performance of a public stock.
        /**Specifics:
        * Method can retrieve details of a stock:
        public float CurrentPrice { get; set; } PASSED
        public DateTime PriceDate { get; set; } PASSED
        public float PriceOpen { get; set; } PASSED
        public float PriceClose { get; set; } PASSED
        public float DayHigh { get; set; }
        public float DayLow { get; set; }
        public float FiftyTwoWeekHigh { get; set; }
        public float FiftyTwoWeekLow { get; set; }
        public float DayAverage { get; set; }
        public int Shares { get; set; }
        public int Volume { get; set; }
        public string CompanyName { get; set; }
             */
            //Assert.AreEqual(2.5f, StockA.CurrentPrice);
            //Assert.AreEqual(2.5f, StockA.PriceOpen);
            //Assert.AreEqual(2.5f, StockA.PriceClose);
            //Assert.AreEqual("30/08/2019 12:00:00 AM", StockB.PriceDate); NOTE: could not figure out the right format..


        }

     
    }
}
