using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using WealthMate.Services;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class StockDetailsTest
    {
        private ObservableCollection<StockHistory> _testCollection;
        private User _user = new User();
        private Stock _stock = new Stock
        {
            Symbol = "ABC", CompanyName = "TestCompany", CurrentPrice = float.MaxValue, PositiveDayReturns = true, DayReturn = float.Epsilon, LastTrade = DateTime.MinValue

        };

        private readonly StockHistory _stockHistory = new StockHistory
        {
            Date = DateTime.MaxValue, DayHigh = float.MinValue, DayLow = float.MaxValue, PriceClose = 12.3f, PriceOpen = 32.1f
        };

        [TestMethod]
        public async Task TestLoadStockHistory()
        {
            Assert.IsNull(_testCollection);

            _testCollection = new ObservableCollection<StockHistory>(await App.Database.GetStockHistoryAsync("AIA.NZ"));

            Assert.IsNotNull(_testCollection);

            Assert.IsInstanceOfType(_testCollection[0].PriceOpen, typeof(float));
            Assert.IsInstanceOfType(_testCollection[0].Date, typeof(DateTime));
            Assert.IsInstanceOfType(_testCollection[0].DayHigh, typeof(float));
            Assert.IsInstanceOfType(_testCollection[0].DayLow, typeof(float));
            Assert.IsInstanceOfType(_testCollection[0].PriceClose, typeof(float));
        }

        [TestMethod]
        public void TestStockProperties()
        {
            Assert.AreEqual("ABC", _stock.Symbol);
            Assert.AreEqual("TestCompany", _stock.CompanyName);
            Assert.AreEqual(float.MaxValue, _stock.CurrentPrice);
            Assert.AreEqual(true, _stock.PositiveDayReturns);
            Assert.AreEqual(float.Epsilon, _stock.DayReturn);
            Assert.AreEqual(DateTime.MinValue.ToLocalTime(), _stock.LastTrade);
        }

        [TestMethod]
        public void TestStockHistoryProperties()
        {
            Assert.AreEqual(DateTime.MaxValue, _stockHistory.Date);
            Assert.AreEqual(float.MinValue, _stockHistory.DayHigh);
            Assert.AreEqual(float.MaxValue, _stockHistory.DayLow);
            Assert.AreEqual(12.3f, _stockHistory.PriceClose);
            Assert.AreEqual(32.1f, _stockHistory.PriceOpen);
        }

        [TestMethod]
        public void TestAddToWatchlist()
        {
            Assert.IsFalse(_user.WatchListStocks.Contains(_stock));

            _user.WatchListStocks.Add(_stock);
            Assert.IsTrue(_user.WatchListStocks.Contains(_stock));

            _user.WatchListStocks.Remove(_stock);
            Assert.IsFalse(_user.WatchListStocks.Contains(_stock));
        }
    }
}
