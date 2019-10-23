using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using WealthMate.Models;

namespace WealthMate.Services
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public DateTime PreviousTradingDate { get; }

        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Stock>().Wait();
            _database.CreateTableAsync<StockHistory>().Wait();
            _database.CreateTableAsync<WatchedStock>().Wait();
            _database.CreateTableAsync<OwnedAsset>().Wait();
            _database.CreateTableAsync<OwnedStock>().Wait();

            PreviousTradingDate = GetPreviousTradingDate();
        }

        // TODO: Handle different timezones
        private static DateTime GetPreviousTradingDate()
        {
            var previousDate = DateTime.Now;

            do
            {
                previousDate = previousDate.AddDays(-1);
            } while (previousDate.DayOfWeek == DayOfWeek.Saturday || previousDate.DayOfWeek == DayOfWeek.Sunday);

            return previousDate.Date;
        }

        #region Stocks

        public Task<List<Stock>> GetStocksAsync()
        {
            return _database.Table<Stock>().ToListAsync();
        }

        public Task<Stock> GetStockAsync(string symbol)
        {
            return _database.Table<Stock>().Where(s => s.Symbol == symbol).FirstOrDefaultAsync();
        }

        public Task<int> SaveStockAsync(Stock stock)
        {
            return _database.InsertOrReplaceAsync(stock);
        }

        public Task<int> DeleteStockAsync(Stock stock)
        {
            return _database.DeleteAsync(stock);
        }

        #endregion

        #region WatchList

        public Task<List<WatchedStock>> GetWatchListAsync()
        {
            return _database.Table<WatchedStock>().ToListAsync();
        }

        public Task<int> SaveWatchListAsync(WatchedStock watchStock)
        {
            return _database.InsertOrReplaceAsync(watchStock);
        }

        public Task<int> DeleteWatchListAsync(WatchedStock watchStock)
        {
            return _database.DeleteAsync(watchStock);
        }

        #endregion

        #region Stock History

        public async Task<List<StockHistory>> GetStockHistoryAsync(string symbol)
        {
            var stockHistory = _database.Table<StockHistory>().Where(s => s.Symbol == symbol).ToListAsync();

            // Fetch stock history from remote database if local stock history table is empty
            if (stockHistory.Result.Count == 0)
                return await DataService.FetchStockHistoryAsync(symbol);

            var lastUpdate = stockHistory.Result.Max(s => s.Date).ToLocalTime();

            // Get latest stock history additions
            if (lastUpdate < PreviousTradingDate)
                await DataService.FetchStockHistoryAsync(symbol, lastUpdate);

            Debug.WriteLine($"Local database ({symbol}). Results: {stockHistory.Result.Count}, last updated at: {lastUpdate:yyyy-MM-dd}, date to check: {PreviousTradingDate:yyyy-MM-dd}");

            return await stockHistory;
        }

        public Task<int> SaveStockHistoryAsync(IEnumerable<StockHistory> stockHistory)
        {
            return _database.InsertAllAsync(stockHistory, typeof(StockHistory));
        }

        public Task<int> DeleteStockHistoryAsync(StockHistory stockHistory)
        {
            return _database.DeleteAsync(stockHistory);
        }

        #endregion

        #region Portfolio

        public Task<List<OwnedAsset>> GetOwnedAssetsAsync()
        {
            return _database.Table<OwnedAsset>().ToListAsync();
        }

        public Task<int> SaveOwnedAssetAsync(OwnedAsset ownedAsset)
        {
            return _database.InsertOrReplaceAsync(ownedAsset);
        }

        public Task<int> DeleteOwnedAssetAsync(OwnedAsset ownedAsset)
        {
            return _database.DeleteAsync(ownedAsset);
        }

        public Task<List<OwnedStock>> GetOwnedStocksAsync()
        {
            return _database.Table<OwnedStock>().ToListAsync();
        }

        public Task<int> SaveOwnedStockAsync(OwnedStock ownedStock)
        {
            return _database.InsertOrReplaceAsync(ownedStock);
        }

        #endregion
    }
}
