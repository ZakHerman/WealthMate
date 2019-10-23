using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class SqLiteTest
    {
        [TestMethod]
        public async Task TestSqliteWatchList()
        {
            var watchedStock = new WatchedStock{Symbol = "TEST"};
            
            await App.Database.SaveWatchListAsync(watchedStock);

            var watchList = await App.Database.GetWatchListAsync();
            var count = watchList.Count;

            Assert.IsTrue(count > 0);

            await App.Database.DeleteWatchListAsync(watchedStock);

            watchList = await App.Database.GetWatchListAsync();
            var newCount = watchList.Count;

            Assert.AreEqual(count - 1, newCount);
        }

        [TestMethod]
        public async Task TestSqliteStock()
        {
            var stock = new Stock {Symbol = "TEST123"};
            await App.Database.DeleteStockAsync(stock);
            var stockList = await App.Database.GetStocksAsync();
            var count = stockList.Count;

            Assert.IsTrue(count > 0);

            await App.Database.SaveStockAsync(stock);

            stockList = await App.Database.GetStocksAsync();
            var addCount = stockList.Count;

            Assert.AreEqual(count + 1, addCount);

            await App.Database.DeleteStockAsync(stock);

            stockList = await App.Database.GetStocksAsync();
            var deleteCount = stockList.Count;

            Assert.AreEqual(count, deleteCount);
        }
    }
}
