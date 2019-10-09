using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WealthMate.Models;

namespace WealthMate.Services
{
    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Stock>().Wait();
            _database.CreateTableAsync<WatchedStock>().Wait();
        }

        public Task<List<Stock>> GetStocksAsync()
        {
            return _database.Table<Stock>().ToListAsync();
        }

        public Task<Stock> GetStockAsync(string symbol)
        {
            return _database.Table<Stock>().Where(i => i.Symbol == symbol).FirstOrDefaultAsync();
        }

        public Task<int> SaveStockAsync(Stock stock)
        {
            return _database.InsertOrReplaceAsync(stock);
        }

        public Task<int> DeleteStockAsync(Stock stock)
        {
            return _database.DeleteAsync(stock);
        }

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
    }
}
